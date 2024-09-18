using MegaChaseGame.Services;
using System;
using UnityEngine;

namespace MegaChaseGame.Behaviours
{
	public sealed partial class KillableOnTooFar : MonoBehaviour
	{
		public event Action Killed;

		private DistanceObserver _distanceObserver;

	    [field: SerializeField] public bool IsAlive { get; private set; } = true;

		public void Initialize(DistanceObserver distanceObserver)
		{
			if (_distanceObserver is not null)
				UnsubscribeFromKillingEvent();

			_distanceObserver = distanceObserver;	
			SubscribeToKillingEvent();

			IsAlive = true;
			gameObject.SetActive(true);
		}

	    public void Kill()
	    {
			if (IsAlive == false)
				throw new LivingStateAlterationImpossibleException($"'{gameObject.name}' is already dead");

			gameObject.SetActive(false);

			IsAlive = false;

			Killed?.Invoke();
	    }

	    public void Respawn()
	    {
	        if (IsAlive)
				throw new LivingStateAlterationImpossibleException($"'{gameObject.name}' is already alive");

			gameObject.SetActive(true);

			IsAlive = true;
		}

		private void SubscribeToKillingEvent() => _distanceObserver.ObjectsAreTooFar += Kill;
		private void UnsubscribeFromKillingEvent() => _distanceObserver.ObjectsAreTooFar -= Kill;

		private void Awake()
		{
			gameObject.SetActive(IsAlive);
		}
		private void OnDisable()
		{
			if (_distanceObserver is not null)
				UnsubscribeFromKillingEvent();
		}
	}
}
