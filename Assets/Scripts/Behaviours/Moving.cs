using System;
using UnityEngine;

namespace MegaChaseGame.Behaviours
{
    public sealed partial class Moving : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private bool _canMove = true;

        [SerializeField] private Constraints _constraints;

        public float Speed => _speed;
        public bool CanMove => _canMove;

        [Obsolete("Is incompatible with " + nameof(Rotating) + " use " + nameof(Move) + " instead")]
        public void MoveTowards(Vector3 direction, float delta = 1f)
        {
            if (CanMove == false)
                throw new MovementImpossibleException($"{nameof(CanMove)} is disabled");

            Vector3 stepMovementRaw = direction.normalized * _speed * delta;
			Vector3 stepMovement = ApplyConstraints(stepMovementRaw);
            transform.Translate(stepMovement);
        }

        public void Move(MovementDirection direction, float delta = 1)
        {
            if (direction == MovementDirection.None)
                return;

            float xMovement = 0, zMovement = 0;

            if ((direction & MovementDirection.Forward) != MovementDirection.None)
                zMovement++;

            if ((direction & MovementDirection.Backward) != MovementDirection.None)
                zMovement--;

            if ((direction & MovementDirection.Right) != MovementDirection.None)
                xMovement++;

            if ((direction & MovementDirection.Left) != MovementDirection.None)
                xMovement--;

            Vector3 movementVector = new Vector3(xMovement, 0, zMovement) * _speed * delta;
            transform.Translate(movementVector);
        }

        private Vector3 ApplyConstraints(Vector3 target)
        {
            float constraintedX = 0, constraintedY = 0, constraintedZ = 0;

            if (_constraints.LockXMovement == false)
                constraintedX = target.x;

			if (_constraints.LockYMovement == false)
				constraintedX = target.y;

			if (_constraints.LockZMovement == false)
				constraintedX = target.z;

			return new Vector3(constraintedX, constraintedY, constraintedZ);
        }

		[System.Serializable]
		public sealed class Constraints
		{
			[field: SerializeField] public bool LockXMovement { get; private set; } = false;
			[field: SerializeField] public bool LockYMovement { get; private set; } = false;
			[field: SerializeField] public bool LockZMovement { get; private set; } = false;
		}
	}
}
