using System;
using UnityEngine;

namespace Resources
{
    public class Resource : MonoBehaviour
    {
        public event Action<Resource> Released;

        public void Release()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            
            transform.SetParent(null);
            
            Released?.Invoke(this);
        }
    }
}