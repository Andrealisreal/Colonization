using TMPro;
using UnityEngine;

namespace UI.Bases
{
    public class BaseViewStatistics : MonoBehaviour
    {
        private const string InitialText = "Количество ресурсов:";
        
        [SerializeField] private TextMeshProUGUI _countText;
        
        private void Start() =>
            _countText.text = InitialText;
        
        public void UpdateCount(int value) =>
            _countText.text = $"{InitialText} {value}";
    }
}