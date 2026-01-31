using Bases;
using TMPro;
using UnityEngine;

namespace UI.Bases
{
    public class BaseViewStatistics : MonoBehaviour
    {
        private const string InitialText = "Количество ресурсов:";

        [SerializeField] private BaseStorage _baseStorage;
        [SerializeField] private TextMeshProUGUI _countText;

        private void Start() =>
            _countText.text = InitialText;

        private void OnEnable() =>
            _baseStorage.CountChanged += UpdateCount;

        private void OnDisable() =>
            _baseStorage.CountChanged -= UpdateCount;

        private void UpdateCount(int value) =>
            _countText.text = $"{InitialText} {value}";
    }
}