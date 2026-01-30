using System;
using UnityEngine;

namespace Bases
{
    public class BaseStorage : MonoBehaviour
    {
        private int _count;

        public event Action<int> CountChanged;

        public void IncreaseCount()
        {
            _count++;
            CountChanged?.Invoke(_count);
        }

        public void DecreaseCount(int amount)
        {
            _count -= amount;
            CountChanged?.Invoke(_count);
        }
    }
}