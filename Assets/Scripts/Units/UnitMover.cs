using UnityEngine;

namespace Units
{
    public class UnitMover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector3 _target;
        private bool _hasTarget;

        private void Update()
        {
            if (_hasTarget == false)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }

        public void Move(Vector3 target)
        {
            _target = target;
            _hasTarget = true;
        }
    }
}