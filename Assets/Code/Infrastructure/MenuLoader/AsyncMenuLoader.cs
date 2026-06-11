using System.Threading.Tasks;
using Code.Infrastructure.LoaderServices;
using Code.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.MenuLoader
{
    public class AsyncMenuLoader : MonoBehaviour //Это бы разделить надо на UI и логику, но мне лень
    {
        [SerializeField] private LoadingProgressBar _progressBar;
        [SerializeField] private Image _imageForURL;
        [SerializeField] private TextMeshProUGUI _textHint;
        
        [SerializeField] private string _textImageURL;
        [SerializeField] private string _pathToText;
        
        private AsyncSceneLoader _asyncSceneLoader;
        private AsyncURLImageLoader _asyncURLImageLoader;
        private AsyncResourcesLoader _asyncResourceLoader;

        public void Construct()
        {
            _asyncSceneLoader = Services.GetService<AsyncSceneLoader>();
            _asyncURLImageLoader = Services.GetService<AsyncURLImageLoader>();
            _asyncResourceLoader = Services.GetService<AsyncResourcesLoader>();
        }

        public async void Load()
        {
            await LoadURLImage();
            await LoadText();
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