using System;
using System.Threading.Tasks;
using Code.Infrastructure.LoaderServices;
using Code.Infrastructure.ServiceLocator;
using UnityEngine;

namespace Code.Infrastructure.Loader
{
    public class SceneLoader : IService
    {
        private const string STANDART_SCENE_NAME = "Dialogue";
        private const string STANDART_URL_IMAGE = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQGHpe5m6VFsD6no-V8nxNs3kufM_kRAGsHWg&s";
        private const string STANDART_PATH_TEXT_ASSET = "HintText";
        
        private const float COUNT_OPERATIONS = 3f;

        public Action OnStartLoad;
        public Action<float> OnLoadedProgress;
        public Action<Sprite> OnLoadedSprite;
        public Action<string> OnLoadedText;
        public Action OnLoadedNextScene;
        
        private readonly ILoadService _loadService;

        public SceneLoader(ILoadService loadService)
        {
            _loadService = loadService;
        }

        public async Task LoadAsync(string pathNextScene,string pathToTextAsset,string urlImage)
        {
            OnStartLoad?.Invoke();
            await LoadURLImage(urlImage);
            await LoadText(pathToTextAsset);
            await LoadScene(pathNextScene);
        }

        private async Task LoadScene(string sceneName) =>
            await _loadService.TryLoad(
                () => _loadService.LoadSceneAsync(sceneName, SetProgress),
                () => _loadService.LoadSceneAsync(STANDART_SCENE_NAME, SetProgress),
                () => OnLoadedNextScene?.Invoke());
        
        private async Task LoadURLImage(string urlImag) =>
            await _loadService.TryLoad<Sprite>(
                () => _loadService.LoadSpriteByURLAsync(urlImag, SetProgress),
                () => _loadService.LoadSpriteByURLAsync(STANDART_URL_IMAGE, SetProgress),
                result =>  OnLoadedSprite?.Invoke(result));
        
        private async Task LoadText(string pathToTextAsset) =>
            await _loadService.TryLoad<TextAsset>(
                () => _loadService.LoadResourceAsync<TextAsset>(pathToTextAsset, SetProgress),
            () => _loadService.LoadResourceAsync<TextAsset>(STANDART_PATH_TEXT_ASSET, SetProgress),
            result =>  OnLoadedText?.Invoke(result.text));
        
        private void SetProgress(float progress) =>
            OnLoadedProgress.Invoke(progress / COUNT_OPERATIONS);
    }
}