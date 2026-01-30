using System;
using UnityEngine;

namespace Units
{
    public class UnitMover : MonoBehaviour
    {
        private const float RotationThreshold = 0.0001f;

        [SerializeField] private float _speed;
        [SerializeField] private float _reachDistance;
        [SerializeField] private float _rotationSpeed;

        private Transform _target;

        private float _currentReachDistance;

        private bool _hasTarget;

        public event Action<Transform> Reached;

        private void Update()
        {
            if (_hasTarget == false)
                return;

            RotateTowardsTarget();
            MoveTowardsTarget();

            if (IsInRange() == false)
                return;

            _hasTarget = false;
            Reached?.Invoke(_target);
        }

        public void Move(Transform target, float? customReachDistance = null)
        {
            _target = target;
            _hasTarget = true;
            _currentReachDistance = customReachDistance ?? _reachDistance;
        }

        private void RotateTowardsTarget()
        {
            var direction = _target.position - transform.position;

            if (direction.sqrMagnitude > RotationThreshold == false)
                return;

            var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void MoveTowardsTarget() =>
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        private bool IsInRange()
        {
            var direction = _target.position - transform.position;
            var distanceSqr = direction.sqrMagnitude;

            return (distanceSqr < _currentReachDistance * _currentReachDistance);
        }
    }
}