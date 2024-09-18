using MegaChaseGame.Behaviours;
using System;

namespace MegaChaseGame.Services
{
	public sealed class GameStateSwitcher : IDisposable
	{
		private Bootstrap _bootstrap;
		private InputHandler _inputHandler;

		private Timer _timer;
		private DistanceGameMessagePrinter _messagePrinter;
		private KillableOnTooFar _player;

		private bool _wasInitialized;
		private bool _wasDisposed;

		// Т.к. объект не МоноБеховский, я не могу совершить отписки от событий в OnDisable(),
		// а НЕ отписываться нельзя, т.к. грозит утечкой памяти. Поэтому имплеменитуем финализатор и IDisposable!
		~GameStateSwitcher()
		{
			if (_wasDisposed == false)
				Dispose();
		}

		public void Dispose()
		{
			if (_wasInitialized)
				UnsubscribeFromServices();

			_wasDisposed = true;
		}

		public void Initialize(Bootstrap bootstrap, InputHandler inputHandler,
			Timer timer, DistanceGameMessagePrinter messagePrinter, KillableOnTooFar player)
		{
			if (_wasInitialized)
				UnsubscribeFromServices();

			_bootstrap = bootstrap;
			_inputHandler = inputHandler;

			_timer = timer;
			_messagePrinter = messagePrinter;
			_player = player;

			_wasInitialized = true;
		}

		public void StartGame()
		{
			if (_player.IsAlive == false)
				_player.Respawn();

			_timer.Run();

			SubscribeToServices();
		}

		public void StopGame()
		{
			_timer.Stop();

			// Здесь отписываемся отдельно, т.к. нужно отписаться не от всего,
			// а только от событий, вызывающих победу/поражение (игра же остановлена)
			UnsubscribeFromAutomaticServices();
		}

		private void OnTimeFinished()
		{
			_messagePrinter.PrintWinningMessage();

			StopGame();
		}

		private void OnPlayerKilled()
		{
			_messagePrinter.PrintLoseMessage();

			StopGame();
		}

		private void OnRestartPressed()
		{
			UnsubscribeFromServices();
			_bootstrap.Initialize();
		}

		private void SubscribeToServices()
		{
			_timer.TimeFinished += OnTimeFinished;
			_player.Killed += OnPlayerKilled;

			_inputHandler.RestartPressed += OnRestartPressed;
		}

		private void UnsubscribeFromServices()
		{
			UnsubscribeFromAutomaticServices();

			UnsubscribeFromPlayerControlledServices();
		}

		private void UnsubscribeFromAutomaticServices()
		{
			_timer.TimeFinished -= OnTimeFinished;
			_player.Killed -= OnPlayerKilled;
		}

		private void UnsubscribeFromPlayerControlledServices()
		{
			_inputHandler.RestartPressed -= OnRestartPressed;
		}
	}
}
