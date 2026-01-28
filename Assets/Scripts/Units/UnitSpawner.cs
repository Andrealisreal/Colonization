using Bases;
using UnityEngine;

namespace Units
{
    public class UnitSpawner : MonoBehaviour
    {
        private const float FullCircleRadians = 2f * Mathf.PI;

        [SerializeField] private Unit _prefab;
        [SerializeField] private Base _base;
        [SerializeField] private float _radius;
        [SerializeField] private int _unitsPerCircle;

        private int _spawnIndex;

        public Unit Spawn()
        {
            var unit = Instantiate(_prefab);

            unit.transform.position = GetPositionAroundBase();
            unit.SetMyBase(_base.Id, _base.transform);

            return unit;
        }

        private Vector3 GetPositionAroundBase()
        {
            var angle = FullCircleRadians * _spawnIndex / _unitsPerCircle;
            var offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * _radius;

            _spawnIndex++;

            return _base.transform.position + offset;
        }
    }
}