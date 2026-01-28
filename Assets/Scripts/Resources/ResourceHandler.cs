using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ResourceHandler : MonoBehaviour
    {
        private readonly Dictionary<int, Queue<Resource>> _baseResources = new();
        private readonly Dictionary<int, HashSet<Resource>> _allResources = new();
        private readonly HashSet<Resource> _globalResources = new();

        public void Add(int id, Resource resource)
        {
            if (resource == null)
                return;

            if (_globalResources.Contains(resource))
                return;

            _allResources.TryAdd(id, new HashSet<Resource>());

            if (_allResources[id].Add(resource) == false)
                return;

            _globalResources.Add(resource);
            _baseResources.TryAdd(id, new Queue<Resource>());
            _baseResources[id].Enqueue(resource);
        }

        public bool TryGetFree(int id, out Resource resource)
        {
            resource = null;

            if (_baseResources.TryGetValue(id, out var queue) == false)
                return false;

            if (queue.Count == 0)
                return false;

            resource = queue.Dequeue();

            return true;
        }

        public void Release(int id, Resource resource)
        {
            if (resource == null)
                return;

            _globalResources.Remove(resource);

            if (_allResources.TryGetValue(id, out var set))
                set.Remove(resource);
        }
    }
}