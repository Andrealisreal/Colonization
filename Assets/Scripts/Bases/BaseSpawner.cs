using Resources;
using UnityEngine;

namespace Bases
{
    public class BaseSpawner : MonoBehaviour
    {
        [SerializeField] private Base _prefab;
        
        private ResourceHandler _resourceHandler;
        
        public void Initialize(ResourceHandler resourceHandler) =>
            _resourceHandler = resourceHandler;

        public Base Spawn(Vector3 position, bool spawnStartUnits)
        {
            var newBase = Instantiate(_prefab, position, Quaternion.identity);
            
            newBase.Initialize(_resourceHandler, spawnStartUnits);
            
            return newBase;
        }
    }
}