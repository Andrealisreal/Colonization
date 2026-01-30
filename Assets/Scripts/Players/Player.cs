using Bases;
using Players.Input;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerRaycaster _raycaster;
        
        private Base _ownerBase;

        private void OnEnable()
        {
            _input.MouseLeftClicked += OnMouseLeftClick;
            _raycaster.GroundClicked += OnGroundClicked;
            _raycaster.BaseClicked += OnBaseClicked;
        }

        private void OnDisable()
        {
            _input.MouseLeftClicked -= OnMouseLeftClick;
            _raycaster.GroundClicked -= OnGroundClicked;
            _raycaster.BaseClicked -= OnBaseClicked;
        }

        private void OnMouseLeftClick() =>
            _raycaster.RaycastToBase();

        private void OnGroundClicked(Vector3 position)
        {
            if (_ownerBase != null)
                _ownerBase.SetFlag(position);
        }

        private void OnBaseClicked(Base ownerBase)
        {
            _ownerBase = ownerBase;
            _ownerBase.SetSelected();
        }
    }
}
