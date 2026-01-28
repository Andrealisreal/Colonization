using Resources;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private BaseScanner _scanner;
        [SerializeField] private BaseBarrack _barrack;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ResourceHandler _resourceHandler;

        private BaseCollector _collector;

        public int Id { get; private set; }
        private static int _nextId = 1;

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
            _collector.Released += _resourceHandler.Release;
        }

        private void OnDisable()
        {
            _scanner.Detected -= OnDetect;
            _collector.Released -= _resourceHandler.Release;
        }

        private void OnDetect(Resource resource) =>
            _resourceHandler.Add(Id, resource);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void DispatchUnitToResource()
        {
            foreach (var unit in _barrack.Units)
            {
                if (unit.IsBusy)
                    continue;

                if (_resourceHandler.TryGetFree(Id, out var freeResource))
                    unit.MoveToTarget(freeResource);
                else
                    break;
            }
        }
    }
}