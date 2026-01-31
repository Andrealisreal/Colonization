using System;
using UnityEngine;

namespace Bases
{
    public class BaseFlag : MonoBehaviour
    {
        [SerializeField] private Flag _prefab;

        private Flag _flagInstance;
        
        public event Action<Transform> Installed;

        public bool IsInstalled { get; private set; }
        
        public void SetToGround(Vector3 position)
        {
            if (_flagInstance == null)
                CreateFlag();

            _flagInstance.transform.position = position;

            IsInstalled = true;
            Installed?.Invoke(_flagInstance.transform);
        }

        private void CreateFlag() =>
            _flagInstance = Instantiate(_prefab);
    }
}