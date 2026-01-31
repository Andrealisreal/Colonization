using System;
using Resources;
using Units;
using UnityEngine;

namespace Bases
{
    public class BaseCollector
    {
        private readonly Collider[] _colliders = new Collider[10];

        private readonly Vector3 _basePosition;
        private readonly LayerMask _layerMask;
        private readonly float _radius;
        private readonly int _ownerBase;

        public BaseCollector(Vector3 basePosition, float radius, LayerMask layerMask, int ownerId)
        {
            _basePosition = basePosition;
            _radius = radius;
            _layerMask = layerMask;
            _ownerBase = ownerId;
        }

        public event Action<int, Resource> Released;

        public void HandleObjects()
        {
            var count = Physics.OverlapSphereNonAlloc(_basePosition, _radius, _colliders, _layerMask);

            if (count == 0)
                return;

            for (var i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out Unit unit) == false)
                    continue;

                if (unit.OwnerBaseId != _ownerBase)
                    continue;

                if (unit.HasResource == false)
                    continue;

                var resource = unit.TakeResource();

                resource.Release();
                unit.Release();
                Released?.Invoke(_ownerBase, resource);
            }
        }
    }
}