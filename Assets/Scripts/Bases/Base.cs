using Resources;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private BaseScanner _scanner;
        [SerializeField] private BaseBarrack _barrack;

        private void OnEnable()
        {
            _scanner.Detected += OnDetect;
        }

        private void OnDisable()
        {
            _scanner.Detected -= OnDetect;
        }
        
        private void OnDetect(Resource resource)
        {
            _barrack.Units[1].Move(resource.transform.position);
        }
    }
}