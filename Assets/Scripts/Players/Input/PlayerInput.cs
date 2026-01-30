using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.Input
{
    public class PlayerInput : MonoBehaviour
    {
        private InputActions _inputAction;
        
        public event Action MouseLeftClicked;

        private void Awake()
        {
            _inputAction = new InputActions();
            
            _inputAction.Player.MouseLeftClick.performed += MouseLeftClick;
        }
        
        private void OnEnable() =>
            _inputAction.Enable();

        private void OnDisable() =>
            _inputAction.Disable();
        
        private void MouseLeftClick(InputAction.CallbackContext context) =>
            MouseLeftClicked?.Invoke();
    }
}