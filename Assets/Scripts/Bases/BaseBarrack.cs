using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Bases
{
    public class BaseBarrack : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _unitSpawner;
        [SerializeField] private int _startSize;

        private readonly List<Unit> _units = new();
        
        public IReadOnlyList<Unit> Units => _units;

        private void Start()
        {
            for (var i = 0; i < _startSize; i++)
                _units.Add(_unitSpawner.Spawn());
        }
    }
}