using MegaChaseGame.Behaviours;
using MegaChaseGame.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace MegaChaseGame.Services
{
	public sealed class Bootstrap : MonoBehaviour
	{
		[SerializeField] private DistanceGameConfig _config;

		[Header("Services")]
		[SerializeField] private InputHandler _inputHandler;
		[SerializeField] private DistanceObserver _distanceObserver;
		private DistanceGameMessagePrinter _messagePrinter;
		private Timer _timer;
		private GameStateSwitcher _gameStateSwitcher;

		[Header("Characters")]
		[SerializeField] private GameObject _playerPrefab;
		[SerializeField] private Transform _playerSpawnPosition;
		private ManuallyControlledMover _playerMover;
		private KillableOnTooFar _playerKiller;

		[Space]

		[SerializeField] private GameObject _npcPrefab;
		[SerializeField] private Transform _npcSpawnPosition;
		private RandomWaypointMover _npc;

		[Header("Location Objects")]
		[SerializeField] private List<Transform> _aiNavigationWaypoints;

		public void Initialize()
		{
			_playerMover ??= Instantiate(_playerPrefab)
				.GetComponent<ManuallyControlledMover>();

			_playerKiller ??= _playerMover
				.GetComponent<KillableOnTooFar>();

			_playerMover.transform.position = _playerSpawnPosition.position;
			_playerMover.transform.rotation = _playerSpawnPosition.rotation;
			_playerMover.Initialize(_inputHandler);

			_playerKiller.Initialize(_distanceObserver);

			_npc ??= Instantiate(_npcPrefab)
				.GetComponent<RandomWaypointMover>();

			_npc.transform.position = _npcSpawnPosition.position;
			_npc.transform.rotation = _npc.transform.rotation;
			_npc.Initialize(_aiNavigationWaypoints);

			_distanceObserver.Initialize(_playerMover.transform, _npc.transform, _config.MaxAllowedDistance);

			_messagePrinter ??= new DistanceGameMessagePrinter();

			_timer ??= new Timer();
			_timer.Initialize(_config.TimeToSurviveSeconds);

			_gameStateSwitcher ??= new GameStateSwitcher();
			_gameStateSwitcher.Initialize(this, _inputHandler, _timer, _messagePrinter, _playerKiller);

			_gameStateSwitcher.StartGame();
		}

		private void Awake()
		{
			Initialize();
		}

		private void OnDisable()
		{
			_gameStateSwitcher?.Dispose();
		}
	}
}
