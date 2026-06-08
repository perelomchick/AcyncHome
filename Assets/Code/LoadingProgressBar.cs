using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class LoadingProgressBar : MonoBehaviour
    {
        private readonly string TEMPLATE_LOADING_TEXT = $"Loading... ({ConstName.LOADING_VALUE_NAME}%)";

        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _progressText;

        public void SetProgress(float value)
        {
            Debug.Log("SetProgress "  + value);
            _progressBar.value = value;
            _progressText.text = TEMPLATE_LOADING_TEXT.Replace(ConstName.LOADING_VALUE_NAME, (value * 100).ToString("F1"));
        }
    }
}