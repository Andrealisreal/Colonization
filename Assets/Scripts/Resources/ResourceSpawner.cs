using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources
{
    public class ResourceSpawner : MonoBehaviour
    {
        private const float MinSpawnRadius = 0;

        [SerializeField] private ResourcePool _resourcePool;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _maxSpawnRadius;
        [SerializeField] private float _interval;

        private WaitForSeconds _wait;

        private void Awake() =>
            _wait = new WaitForSeconds(_interval);

        private void Start() =>
            StartCoroutine(SpawnRoutine());

        private void Spawn()
        {
            var item = _resourcePool.GetResource();

            item.transform.position = GetSpawnPosition() + GetSpawnOffset();
        }

        private Vector3 GetSpawnPosition()
        {
            var randomIndex = Random.Range(0, _spawnPoints.Length);

            return _spawnPoints[randomIndex].position;
        }

        private Vector3 GetSpawnOffset()
        {
            var x = Random.Range(MinSpawnRadius, _maxSpawnRadius);
            var z = Random.Range(MinSpawnRadius, _maxSpawnRadius);

            return new Vector3(x, 0, z);
        }

        private IEnumerator SpawnRoutine()
        {
            while (enabled)
            {
                yield return _wait;
                
                Spawn();
            }
        }
    }
}