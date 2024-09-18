using System;
using System.Collections;
using UnityEngine;

namespace MegaChaseGame.Services
{
	public sealed class Timer
	{
	    public event Action TimeFinished;

	    public const float TickSeconds = 0.01f;

		private float _durationSeconds;
		private float _timeRemaining;

		// Я правильно назвал, или readonly, как и const, пишем в PascalCase?
		private readonly CoroutinesHandler _coroutinesHandler = CoroutinesHandler.Instance; 
		private Coroutine _ticksCountingRoutine;

		public void Initialize(float durationSeconds, bool runAfterInitializing = false)
		{
			if (CurrentState == State.Running || CurrentState == State.Paused)
				Stop();
			else
				CurrentState = State.Stopped;

			_durationSeconds = durationSeconds;

			if (runAfterInitializing)
				Run();
		}

	    public State CurrentState { get; private set; } = State.Stopped;

		public void Run()
	    {
	        if (CurrentState != State.Stopped && CurrentState != State.Finished)
	            throw new InvalidOperationException("Cannot run a non-stopped timer");

			_timeRemaining = _durationSeconds;

			_coroutinesHandler.StartCoroutine(ProcessTick());

			CurrentState = State.Running;
		}

	    public void Stop()
	    {
			if (CurrentState != State.Running && CurrentState != State.Paused)
				throw new InvalidOperationException("Cannot stop a timer which is not currently working");

			_coroutinesHandler.StopCoroutine(_ticksCountingRoutine);

			_timeRemaining = 0;

			CurrentState = State.Stopped;
		}

	    public void Pause()
	    {
			if (CurrentState != State.Running)
				throw new InvalidOperationException("Cannout pause a timer which is not currently working");

			_coroutinesHandler.StopCoroutine(_ticksCountingRoutine);

			CurrentState = State.Paused;
		}

	    public void Unpause()
	    {
			if (CurrentState != State.Paused)
				throw new InvalidOperationException("Cannot unpause a non-paused timer");

			_coroutinesHandler.StartCoroutine(ProcessTick());

			CurrentState = State.Running;
		}

		private IEnumerator ProcessTick()
		{
			yield return new WaitForSecondsRealtime(TickSeconds);
			_timeRemaining -= TickSeconds;

			if (_timeRemaining > 0)
			{
				_ticksCountingRoutine = _coroutinesHandler.StartCoroutine(ProcessTick());
			}
			else
			{
				TimeFinished?.Invoke();
				CurrentState = State.Finished;
			}
		}

		public enum State { None, Stopped, Running, Paused, Finished }
	}
}
