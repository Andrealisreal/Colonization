using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Resources
{
    public class ResourcePool : MonoBehaviour
    {
        [SerializeField] private int _startSize;
        [SerializeField] private Resource _prefab;

        private readonly List<Resource> _pool = new();

        private void Start()
        {
            for (var i = 0; i < _startSize; i++)
                _pool.Add(Create());
        }

        public Resource GetResource()
        {
            var resource = _pool.FirstOrDefault(item => item.gameObject.activeSelf == false);

            if (resource == null)
                resource = Create();

            resource.gameObject.SetActive(true);
            resource.Released += OnRelease;

            return resource;
        }

        private static void OnRelease(Resource resource)
        {
            resource.Released -= OnRelease;
            resource.transform.position = Vector3.zero;
            resource.transform.rotation = Quaternion.identity;
            resource.transform.SetParent(null);
            resource.gameObject.SetActive(false);
        }

        private Resource Create()
        {
            var resource = Instantiate(_prefab);

            resource.gameObject.SetActive(false);
            _pool.Add(resource);

            return resource;
        }
    }
}