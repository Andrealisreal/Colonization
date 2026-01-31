using Resources;
using UI.Bases;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        private const int ConstructionCost = 5;

        [SerializeField] private BaseStorage _storage;
        [SerializeField] private BaseScanner _scanner;
        [SerializeField] private BaseBarrack _barrack;
        [SerializeField] private BaseViewStatistics _statistics;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private BaseFlag _baseFlag;

        private Transform _flagPosition;
        private ResourceHandler _resourceHandler;

        private BaseState _state = BaseState.Idle;

        private BaseCollector _collector;

        private bool _isSelected;
        private static int _nextId = 1;

        public int Id { get; private set; }

        private void Awake()
        {
            Id = _nextId++;
            _collector = new BaseCollector(transform.position, _radius, _layerMask, Id);
        }

        private void Update()
        {
            _collector.HandleObjects();

            DispatchUnitToResource();
        }

        private void OnEnable()
        {
            _scanner.Detected += OnDetect;
            _storage.CountChanged += OnCountChanged;
            _baseFlag.Installed += OnInstalled;
        }

        private void OnDisable()
        {
            _scanner.Detected -= OnDetect;
            _collector.Released -= _resourceHandler.Release;
            _storage.CountChanged -= OnCountChanged;
            _baseFlag.Installed -= OnInstalled;
        }

        public void Initialize(ResourceHandler resourceHandler, bool spawnStartUnits)
        {
            _resourceHandler = resourceHandler;
            _barrack.Initialize(spawnStartUnits);
            _collector.Released += OnReleased;
        }

        public void SetFlag(Vector3 position)
        {
            if (_isSelected == false)
                return;

            if (_barrack.Units.Count < 2)
                return;

            _baseFlag.SetToGround(position);
            _isSelected = false;
        }

        public void SetSelected() =>
            _isSelected = true;

        private void OnDetect(Resource resource) =>
            _resourceHandler.Add(Id, resource);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void OnReleased(int id, Resource resource)
        {
            _resourceHandler.Release(id, resource);
            _storage.IncreaseCount();
        }

        private void OnCountChanged(int currentCount)
        {
            switch (_state)
            {
                case BaseState.Idle:
                    HandleIdleState(currentCount);
                    break;

                case BaseState.WaitingConstruction:
                    HandleConstructionState(currentCount);
                    break;
            }
        }

        private void HandleIdleState(int currentCount)
        {
            if (currentCount < _barrack.AmountResourcesSpawn)
                return;

            _storage.DecreaseCount(_barrack.AmountResourcesSpawn);
            _barrack.Spawn();
        }

        private void HandleConstructionState(int currentCount)
        {
            if (currentCount < ConstructionCost)
                return;

            _storage.DecreaseCount(ConstructionCost);
            SendUnitToBuild();
            _state = BaseState.Idle;
        }

        private void SendUnitToBuild()
        {
            foreach (var unit in _barrack.Units)
            {
                if (unit.IsBusy)
                    continue;

                _barrack.RemoveUnit(unit);
                unit.MoveToFlag(_flagPosition);

                return;
            }
        }

        private void DispatchUnitToResource()
        {
            foreach (var unit in _barrack.Units)
            {
                if (unit.IsBusy)
                    continue;

                if (_resourceHandler.TryGetFree(Id, out var freeResource))
                    unit.MoveToResource(freeResource);
                else
                    break;
            }
        }

        private void OnInstalled(Transform flagPosition)
        {
            _flagPosition = flagPosition;
            _state = BaseState.WaitingConstruction;
        }
    }
}