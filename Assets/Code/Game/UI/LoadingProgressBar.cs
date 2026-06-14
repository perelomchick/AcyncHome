using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.UI
{
    public class LoadingProgressBar : MonoBehaviour
    {
        private const string LOADING_VALUE_NAME = "{Loading}";
        
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _progressText;

        private readonly string _templateLoadingText = $"Loading... ({LOADING_VALUE_NAME}%)";

        public void SetProgress(float value)
        {
            _progressBar.value = value;
            _progressText.text = _templateLoadingText.Replace(LOADING_VALUE_NAME, (value * 100).ToString("F1"));
        }
    }
}