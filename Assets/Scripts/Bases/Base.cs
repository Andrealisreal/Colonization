using System;
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

        private ResourceHandler _resourceHandler;
        private BaseCollector _collector;

        private void Awake()
        {
            _resourceHandler = new ResourceHandler();
            _collector = new BaseCollector(transform.position, _radius, _layerMask, this);
        }

        private void FixedUpdate() =>
            _collector.HandleObjects();

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
        
        private void OnDetect(Resource resource)
        {
            _resourceHandler.Add(resource);
            DispatchUnitToResource(resource);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void DispatchUnitToResource(Resource resource)
        {
            foreach (var unit in _barrack.Units)
            {
                if(unit.IsBusy)
                    continue;
                
                if(_resourceHandler.TryGetFree(out var freeResource) == false)
                    return;
                
                unit.MoveToTarget(resource);
            }
        }
    }
}