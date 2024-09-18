using UnityEngine;

namespace MegaChaseGame.Behaviours
{
	[RequireComponent(typeof(Moving))]
	[RequireComponent(typeof(Rotating))]
	public sealed class ManuallyControlledMover : MonoBehaviour
	{
		private Moving _movingBehaviour;
		private Rotating _rotatingBehaviour;
		private InputHandler _inputHandler;

		public void Initialize(InputHandler inputHandler)
		{
			// Unsubscribing from a previously attached InputHandler (if there is such)
			if (_inputHandler is not null)
				UnsubscribeFromInputHander();

			_inputHandler = inputHandler;

			SubscribeToInputHander();
		}

		private void OnUpPressed(float delta) => _movingBehaviour.Move(MovementDirection.Forward, delta);
		private void OnDownPressed(float delta) => _movingBehaviour.Move(MovementDirection.Backward, delta);
		private void OnLeftPressed(float delta) => _rotatingBehaviour.Rotate(RotationDirection.Left, delta);
		private void OnRightPressed(float delta) => _rotatingBehaviour.Rotate(RotationDirection.Right, delta);

		private void SubscribeToInputHander()
		{
			_inputHandler.UpPressed += OnUpPressed;
			_inputHandler.DownPressed += OnDownPressed;
			_inputHandler.LeftPressed += OnLeftPressed;
			_inputHandler.RightPressed += OnRightPressed;
		}

		private void UnsubscribeFromInputHander()
		{
			_inputHandler.UpPressed -= OnUpPressed;
			_inputHandler.DownPressed -= OnDownPressed;
			_inputHandler.LeftPressed -= OnLeftPressed;
			_inputHandler.RightPressed -= OnRightPressed;
		}

		private void Awake()
		{
			_movingBehaviour = GetComponent<Moving>();
			_rotatingBehaviour = GetComponent<Rotating>();
		}

		private void OnDisable()
		{
			if (_inputHandler is not null)
				UnsubscribeFromInputHander();
		}
	}
}
