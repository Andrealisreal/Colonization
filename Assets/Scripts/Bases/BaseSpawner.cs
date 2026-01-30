using Resources;
using UnityEngine;

namespace Bases
{
    public class BaseSpawner : MonoBehaviour
    {
        [SerializeField] private Base _prefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private ResourceHandler _resourceHandler;

        private void Start() =>
            Spawn(_startPoint.position, true);

        public Base Spawn(Vector3 position, bool spawnStartUnits)
        {
            var newBase = Instantiate(_prefab, position, Quaternion.identity);
            
            newBase.Initialize(_resourceHandler);
            newBase.Barrack.Initialize(spawnStartUnits);
            
            return newBase;
        }
    }
}