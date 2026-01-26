using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitMover _mover;
        
        public void Move(Vector3 target) =>
            _mover.Move(target);
    }
}