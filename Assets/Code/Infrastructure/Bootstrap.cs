using Code.Infrastructure.LoaderServices;
using Code.Infrastructure.MenuLoader;
using Code.ServiceLocator;
using UnityEngine;


namespace Code.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
         [SerializeField] private AsyncMenuLoader _menuLoader;
        
        private void Awake()
        {
            Services.Register<AsyncSceneLoader>(new AsyncSceneLoader());
            Services.Register<AsyncURLImageLoader>(new AsyncURLImageLoader());
            Services.Register<AsyncResourcesLoader>(new AsyncResourcesLoader());
            
            _menuLoader.Construct();
            _menuLoader.Load();
        }
    }
}