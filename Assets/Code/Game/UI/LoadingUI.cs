using System.Collections;
using System.Threading.Tasks;
using Code.Infrastructure.Loader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.UI
{
    public class LoadingUI : MonoBehaviour
    {
        private const float FADE_DURATION = 0.5f;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LoadingProgressBar _progressBar;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _textHint;

        private SceneLoader _sceneLoader;
        
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            _sceneLoader.OnStartLoad += () => StartCoroutine(StartLoading(0, 1f));
            _sceneLoader.OnLoadedNextScene += () => StartCoroutine(StartLoading(1f, 0));
            _sceneLoader.OnLoadedProgress += SetProgressBar;
            _sceneLoader.OnLoadedText += SetText;
            _sceneLoader.OnLoadedSprite += SetSprite;
        }
        

        private IEnumerator StartLoading(float from, float to)
        {    
            _canvasGroup.alpha = from;
            float elapsedTime = 0f;

            while (elapsedTime < FADE_DURATION)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(from, to, elapsedTime / FADE_DURATION);
                yield return null;
            }

            _canvasGroup.alpha = to;
        }

        private void Awake() => DontDestroyOnLoad(gameObject);
        
        private void SetProgressBar(float value) => _progressBar.SetProgress(value);
        
        private void SetSprite(Sprite value) => _image.sprite = value;
        
        private void SetText(string value) => _textHint.text = value;
        
        
        
    }
}