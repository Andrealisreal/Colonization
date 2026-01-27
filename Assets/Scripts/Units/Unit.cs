using Bases;
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
        
        public bool HasResource { get; private set; }
        public bool IsBusy { get; private set; }
        public Base MyBase { get; private set; }

        private void OnEnable()
        {
            _mover.Reached += _collector.CatchUp;
            _collector.Raised += ReturnToBase;
        }

        private void OnDisable()
        {
            _mover.Reached -= _collector.CatchUp;
        }

        public void MoveToTarget(Resource resource)
        {
            IsBusy = true;
            _mover.Move(resource.transform);
        }

        public Resource TakeResource() =>
            _currentResource;

        public void SetMyBase(Base myBase) =>
            MyBase = myBase;

        public void Release()
        {
            IsBusy = false;
            HasResource = false;
        }

        private void ReturnToBase(Resource resource)
        {
            _currentResource = resource;
            _collector.Raised -= ReturnToBase;
            HasResource = true;
            _mover.Move(MyBase.transform, _reachDistanceBase);
        }
    }
}