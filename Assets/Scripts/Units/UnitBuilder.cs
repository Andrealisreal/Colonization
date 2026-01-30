using System;
using Bases;
using UnityEngine;

namespace Units
{
    public class UnitBuilder : MonoBehaviour
    {
        private BaseSpawner _baseSpawner;

        public event Action<int, Transform> Builded;
        
        private void Awake() =>
            _baseSpawner = FindAnyObjectByType<BaseSpawner>();
        
        public void Build(Transform flagPosition)
        {
            var newBase = _baseSpawner.Spawn(flagPosition.position, spawnStartUnits: false);

            if (TryGetComponent(out Unit unit))
                newBase.Barrack.AddUnit(unit);

            Builded?.Invoke(newBase.Id, flagPosition);
        }
    }
}
