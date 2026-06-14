using System;
using System.Threading.Tasks;
using Code.Game.Data;
using Code.Infrastructure.ServiceLocator;
using UnityEngine;

namespace Code.Infrastructure.LoaderServices
{
    public interface ILoadService : IService
    {
        Task<T> LoadResourceAsync<T>(string path, Action<float> onProgress = null) where T : UnityEngine.Object;
        Task LoadSceneAsync(string sceneName, Action<float> onProgress = null);
        Task<Sprite> LoadSpriteByURLAsync(string url, Action<float> onProgress = null);
        Task<CharacterData> LoadCharacterDataAsync(string path, Action<float> onProgress = null);
        Task TryLoad(Func<Task> load, Func<Task> fallback, Action onSuccess);
        Task TryLoad<T>(Func<Task<T>> load, Func<Task<T>> fallback, Action<T> onSuccess);
    }
}