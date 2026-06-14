using Code.Game.UI;
using Code.Infrastructure.Loader;
using Code.Infrastructure.LoaderServices;
using Code.Infrastructure.ServiceLocator;
using UnityEngine;


namespace Code.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LoadingUI _loadingUI;
        
        [SerializeField] private string _pathNextScene;
        [SerializeField] private string _pathToTextAsset;
        [SerializeField] private string _urlImage;
        
        private void Awake()
        {
            RegisterServices();
            
            var sceneLoader = Services.GetService<SceneLoader>();
            _loadingUI.Construct(sceneLoader);
            sceneLoader.LoadAsync(_pathNextScene, _pathToTextAsset, _urlImage);
        }

        private void RegisterServices()
        {
            Services.Register<ILoadService>(new LoadService());
            Services.Register(new SceneLoader(Services.GetService<ILoadService>()));
        }
    }
}