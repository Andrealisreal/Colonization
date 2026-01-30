using System;
using UnityEngine;

namespace Bases
{
    public class BaseFlag : MonoBehaviour
    {
        [SerializeField] private GameObject _flagPrefab;

        public event Action<Transform> Installed;

        public bool IsInstalled { get; private set; }

        private GameObject _flagInstance;

        public void SetToGround(Vector3 position)
        {
            if (_flagInstance == null)
                CreateFlag();

            _flagInstance.transform.position = position;

            IsInstalled = true;
            Installed?.Invoke(_flagInstance.transform);
        }

        private void CreateFlag() =>
            _flagInstance = Instantiate(_flagPrefab);
    }
}