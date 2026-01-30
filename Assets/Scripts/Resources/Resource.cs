using System;
using UnityEngine;

namespace Resources
{
    public class Resource : MonoBehaviour
    {
        public event Action<Resource> Released;

        public void Release() =>
            Released?.Invoke(this);
    }
}