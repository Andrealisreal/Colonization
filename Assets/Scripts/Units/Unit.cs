using Resources;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitMover _mover;
        [SerializeField] private UnitCollector _collector;
        [SerializeField] private float _reachDistanceBase;

        private Resource _currentResource;
        private Transform _basePosition;

        public bool HasResource { get; private set; }
        public bool IsBusy { get; private set; }
        public int MyBaseId { get; private set; }

        public void MoveToTarget(Resource resource)
        {
            IsBusy = true;
            _mover.Reached += _collector.CatchUp;
            _collector.Raised += ReturnToBase;
            _mover.Move(resource.transform);
        }

        public Resource TakeResource() =>
            _currentResource;

        public void SetMyBase(int baseId, Transform basePosition)
        {
            _basePosition = basePosition;
            MyBaseId = baseId;
        }

        public void Release()
        {
            IsBusy = false;
            HasResource = false;
        }

        private void ReturnToBase(Resource resource)
        {
            _mover.Reached -= _collector.CatchUp;
            _collector.Raised -= ReturnToBase;
            _currentResource = resource;
            HasResource = true;
            _mover.Move(_basePosition, _reachDistanceBase);
        }
    }
}