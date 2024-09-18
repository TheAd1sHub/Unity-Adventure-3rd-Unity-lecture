#define EXPERIMENTAL_MOVEMENT

using System.Collections.Generic;
using UnityEngine;

namespace MegaChaseGame.Behaviours
{
    [RequireComponent(typeof(Moving))]
    [RequireComponent(typeof(Rotating))]
    public sealed class RandomWaypointMover : MonoBehaviour
    {
        [SerializeField] private float _сollisionDistance = 0.05f;
        [SerializeField] private List<Transform> _waypoints = new();

        private Vector3 _curTargetPosition;
        private Moving _movingBehaviour;
        private Rotating _rotationBehaviour;

        public Vector3 TargetDirection
        {
            get
            {
                //float movementX = _curTargetPosition.x - transform.position.x;
                //
                //float movementY = _curTargetPosition.y - transform.position.y;
                //
                //float movementZ = _curTargetPosition.z - transform.position.z;
                //
                //return new Vector3(movementX, movementY, movementZ);

                return _curTargetPosition - transform.position;
            }
        }

        public Vector3 RotationFromTarget
        {
            get
            {
                Vector3 result = _curTargetPosition - transform.rotation.eulerAngles;
                // result.x = 0;
                // result.z = 0;

                return result;

                //float rotationX = _movementConstraints.LockXRotation
                //    ? 0
                //    : transform.rotation.x - _curTargetPosition
                //
                //return new Vector3(rotationX, rotationY, rotationZ);
            }
        }

        public void Initialize(List<Transform> waypoints)
        {
            _waypoints = waypoints;
			_curTargetPosition = GetNextTargetWaypointPosition();
		}

        private Vector3 GetNextTargetWaypointPosition()
        {
            Vector3 nextTarget = _curTargetPosition;

            do
            {
                int nextWaypointIndex = Random.Range(0, _waypoints.Count);
                nextTarget = _waypoints[nextWaypointIndex].position;
            }
            while (nextTarget == _curTargetPosition);
        
            return nextTarget;
        }

        private void MoveTowardsCurTarget()
        {
			float distanceToTarget = TargetDirection.magnitude;

			if (distanceToTarget > _сollisionDistance)
				_movingBehaviour.Move(MovementDirection.Forward, delta: Time.deltaTime);
			else
				_curTargetPosition = GetNextTargetWaypointPosition();
		}

        private void RotateTowardsCurTarget()
        {
            _rotationBehaviour.RotateTowards(TargetDirection, delta: Time.deltaTime);
            
        }

        private void Awake()
        {
            _movingBehaviour = GetComponent<Moving>();
            _rotationBehaviour = GetComponent<Rotating>();
		}

        private void Update()
        {
            if (_movingBehaviour.CanMove)
                MoveTowardsCurTarget();

            if (_rotationBehaviour.CanRotate)
                RotateTowardsCurTarget();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Target direction
            Vector3 lineStart = transform.position;
            Vector3 lineEnd = transform.position + TargetDirection.normalized;
            Debug.DrawLine(lineStart, lineEnd, Color.cyan);
            
            Gizmos.DrawSphere(_curTargetPosition, 0.1f);
            
        }
#endif
    }
}