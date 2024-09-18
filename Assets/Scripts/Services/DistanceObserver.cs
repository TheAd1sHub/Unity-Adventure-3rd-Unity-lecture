using System;
using UnityEngine;

namespace MegaChaseGame.Services
{
	public sealed class DistanceObserver : MonoBehaviour
	{
		public event Action ObjectsAreTooFar;

		private float _allowedDistance;

		private Transform _target1, _target2;

		public void Initialize(Transform target1, Transform target2, float allowedDistance)
		{
			_target1 = target1;
			_target2 = target2;

			_allowedDistance = allowedDistance;
		}

		private void Update()
		{
			float distanceBetweenObjects = Vector3.Distance(_target1.position, _target2.position);

			if (distanceBetweenObjects > _allowedDistance)
				ObjectsAreTooFar?.Invoke();
		}
	}
}
