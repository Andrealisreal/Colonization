using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Bases
{
    public class BaseBarrack : MonoBehaviour
    {
        [field: SerializeField] public int AmountResourcesSpawn { get; private set; }
        
        [SerializeField] private UnitSpawner _unitSpawner;
        [SerializeField] private int _startSize;

        private readonly List<Unit> _units = new();
        
        public IReadOnlyList<Unit> Units => _units;

        public void Initialize(bool spawnStartUnits)
        {
            if (spawnStartUnits == false)
                return;

            for (var i = 0; i < _startSize; i++)
                _units.Add(_unitSpawner.Spawn());
        }

        public void Spawn() =>
            _units.Add(_unitSpawner.Spawn());

        public void RemoveUnit(Unit unit) =>
            _units.Remove(unit);
        
        public void AddUnit(Unit unit) =>
            _units.Add(unit);
    }
}