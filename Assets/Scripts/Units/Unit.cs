using Resources;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitMover _mover;
        [SerializeField] private UnitCollector _collector;
        [SerializeField] private float _reachDistanceBase;
        [SerializeField] private UnitBuilder _builder;

        private Resource _currentResource;
        private Transform _basePosition;

        public bool HasResource { get; private set; }
        public bool IsBusy { get; private set; }
        public int MyBaseId { get; private set; }

        private void OnEnable() =>
            _builder.Builded += OnBuilded;

        private void OnDisable() =>
            _builder.Builded -= OnBuilded;

        public void MoveToResource(Resource resource)
        {
            IsBusy = true;
            _mover.Reached += _collector.CatchUp;
            _collector.Raised += ReturnToBase;
            _mover.Move(resource.transform);
        }

        public void MoveToFlag(Transform flagPosition)
        {
            _mover.Reached += OnFlagReached;
            IsBusy = true;
            _mover.Move(flagPosition);
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

        private void OnFlagReached(Transform flagPosition)
        {
            _mover.Reached -= OnFlagReached;
            _builder.Build(flagPosition);
        }

        private void OnBuilded(int baseId, Transform basePosition)
        {
            SetMyBase(baseId, basePosition);
            Release();
        }
    }
}