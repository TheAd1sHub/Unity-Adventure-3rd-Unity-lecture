using System;
using UnityEngine;

namespace MegaChaseGame.Behaviours
{
	public sealed partial class Rotating : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _rotationSpeed = 1f;
        [SerializeField] private bool _canRotate = true;

		[SerializeField] private Constraints _constraints;

        public float RotationSpeed => _rotationSpeed;
        public bool CanRotate => _canRotate;

		public void Rotate(RotationDirection direction, float delta = 1f)
		{
			if (direction == RotationDirection.None)
				throw new ArgumentException($"Cannot process '{nameof(RotationDirection.None)}' direction");

			float yRotation = (int)direction * _rotationSpeed * delta;
			Vector3 rotation = new Vector3(0, yRotation, 0);

			Vector3 appliedRotationRaw = Vector3.up * (int)direction * _rotationSpeed;
			Vector3 appliedRotation = ApplyConstraints(appliedRotationRaw);

			transform.Rotate(appliedRotation);
		}

        public void RotateTowards(Vector3 targetPosition, float delta = 1f)
        {
            if (CanRotate == false)
                throw new RotationImpossibleException();

			//Vector3 lookAtPointRaw = _rotationSpeed * delta * (targetPosition - transform.position).normalized;
			//Vector3 lookAtPoint = ApplyConstraints(lookAtPointRaw);
			//
			//Debug.Log($"Raw: {targetPosition - transform.position * delta} | Processed: {lookAtPoint}");
			
			//Vector3 newRotationRaw = Vector3.MoveTowards(transform.rotation.eulerAngles, targetPosition, _rotationSpeed * delta);
			//Vector3 newRotation = ApplyConstraints(newRotationRaw);
			Vector3 newRotation = ApplyConstraints(targetPosition);
			
			//Vector3 newRotation = ApplyConstraints(targetPosition);

			if (newRotation == transform.rotation.eulerAngles)
				return;

			transform.rotation = Quaternion.LookRotation(targetPosition);
        }

		private Vector3 ApplyConstraints(Vector3 target)
		{
			float constraintedX = 0, constraintedY = 0, constraintedZ = 0;

			if (_constraints.LockXRotation == false)
				constraintedX = target.x;

			if (_constraints.LockYRotation == false)
				constraintedY = target.y;

			if (_constraints.LockZRotation == false)
				constraintedZ = target.z;

			return new Vector3(constraintedX, constraintedY, constraintedZ);
		}

		[System.Serializable]
		public sealed class Constraints
		{
			[field: SerializeField] public bool LockXRotation { get; private set; } = false;
			[field: SerializeField] public bool LockYRotation { get; private set; } = false;
			[field: SerializeField] public bool LockZRotation { get; private set; } = false;
		}
	}
}
