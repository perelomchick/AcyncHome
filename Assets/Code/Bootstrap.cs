using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Code
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LoadingProgressBar _progressBar;
        [SerializeField] private Image _imageForURL;
        [SerializeField] private TextMeshProUGUI _textHint;
        
        [SerializeField] private string _textImageURL;
        [SerializeField] private string _pathToText;
        
        private readonly AsyncSceneLoader _asyncSceneLoader = new AsyncSceneLoader();
        private readonly AsyncURLImageLoader _asyncURLImageLoader = new AsyncURLImageLoader();
        private readonly AsyncResourcesLoader _asyncResourceLoader = new AsyncResourcesLoader();
        
        
        private async void Start()
        {
            await LoadText();
            await LoadURLImage();
            await NeedLookAtCat();
            await LoadScene();
        }

        private async Task LoadScene() =>
            await _asyncSceneLoader.LoadScene(ConstName.DIALOGUE_SCENE_NAME,x => _progressBar.SetProgress(x));

        private async Task LoadURLImage()
        {
            var taskURL =  _asyncURLImageLoader.LoadingImageByURL(_textImageURL, x => _progressBar.SetProgress(x));
            await taskURL;
            _imageForURL.sprite = taskURL.Result;
        }

        private async Task LoadText()
        {
            Task<TextAsset> taskText = _asyncResourceLoader.LoadResources<TextAsset>(_pathToText,x => _progressBar.SetProgress(x));
            await taskText;
            _textHint.text = taskText.Result.text;
        }

        private async Task NeedLookAtCat() => await Task.Delay(3000);
    }
}