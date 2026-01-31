using System;
using Bases;
using UnityEngine;

namespace Units
{
    public class UnitBuilder : MonoBehaviour
    {
        [SerializeField] private BaseSpawner _baseSpawner;

        public event Action<int, Transform> Builded;
        
        public void Build(Transform flagPosition, Unit unit)
        {
            var newBase = _baseSpawner.Spawn(flagPosition.position, spawnStartUnits: false);
            
            if (newBase.TryGetComponent(out BaseBarrack baseBarrack) == false)
                return;
            
            baseBarrack.AddUnit(unit);
            Builded?.Invoke(newBase.Id, flagPosition);
        }
    }
}
