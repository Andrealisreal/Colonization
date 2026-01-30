using System;
using Bases;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class PlayerRaycaster : MonoBehaviour
    {
        [SerializeField] private float _raycastDistance;
        [SerializeField] private LayerMask _layerMask;

        private RaycastHit _hit;
        private Camera _camera;

        public event Action<Base> BaseClicked;
        public event Action<Vector3> GroundClicked;

        private void Awake() =>
            _camera = Camera.main;

        public void RaycastToBase()
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out _hit, _raycastDistance, _layerMask) == false)
                return;

            if (_hit.collider.gameObject.TryGetComponent<Base>(out var hitBase))
                BaseClicked?.Invoke(hitBase);

            if (_hit.collider.gameObject.TryGetComponent<Ground>(out var ground))
                GroundClicked?.Invoke(_hit.point);
        }
    }
}