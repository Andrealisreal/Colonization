using System;
using System.Collections;
using Resources;
using UnityEngine;

namespace Bases
{
    public class BaseScanner : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radius;
        [SerializeField] private float _interval;

        private readonly Collider[] _colliders = new Collider[5];

        private WaitForSeconds _wait;

        public event Action<Resource> Detected;

        private void Awake() =>
            _wait = new WaitForSeconds(_interval);

        private void Start() =>
            StartCoroutine(ScanRoutine());

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void Scan()
        {
            var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layerMask);

            for (var i = 0; i < count; i++)
                if (_colliders[i].gameObject.TryGetComponent(out Resource resource))
                    Detected?.Invoke(resource);
        }

        private IEnumerator ScanRoutine()
        {
            while (enabled)
            {
                Scan();

                yield return _wait;
            }
        }
    }
}