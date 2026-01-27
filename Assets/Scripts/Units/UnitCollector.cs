using System;
using Resources;
using UnityEngine;

namespace Units
{
    public class UnitCollector : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        public event Action<Resource> Raised;
        
        public void CatchUp(Transform resource)
        {
            resource.SetParent(transform);
            resource.localPosition = _offset;
            resource.gameObject.TryGetComponent(out Resource resourceComponent);
            Raised?.Invoke(resourceComponent);
        }
    }
}
