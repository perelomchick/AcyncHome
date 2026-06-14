using System;
using System.Threading.Tasks;
using Code.Game.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.LoaderServices
{
    public class LoadService : ILoadService
    {
        public async Task<T> LoadResourceAsync<T>(string path, Action<float> onProgress = null) where T : UnityEngine.Object
        {
            try
            {
                ResourceRequest request = Resources.LoadAsync<T>(path);

                while (!request.isDone)
                {
                    onProgress?.Invoke(request.progress);
                    await Task.Yield();
                }

                onProgress?.Invoke(1f);
                return request.asset as T;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load asset at path '{path}': {e.Message}");
                return null;
            }
        }

        public async Task LoadSceneAsync(string sceneName, Action<float> onProgress = null)
        {
            try
            {
                var  nextScene = SceneManager.LoadSceneAsync(sceneName);
                nextScene.allowSceneActivation = false;

                while (nextScene.progress < 0.9f)
                {
                    onProgress?.Invoke(nextScene.progress);
                    await Task.Yield();
                }
            
                onProgress?.Invoke(1f);
            
                await Task.Yield();
                nextScene.allowSceneActivation = true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to load scene by name '{sceneName}': {e.Message}");
                throw;
            }
        }

        public async Task<Sprite> LoadSpriteByURLAsync(string url, Action<float> onProgress = null)
        {
            try
            {
                using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
                request.SendWebRequest();

                while (!request.isDone)
                {
                    onProgress?.Invoke(request.downloadProgress);
                    await Task.Yield();
                }
            
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to load sprite at url '{url}': {e.Message}");
                throw;
            }
        }

        public async Task<CharacterData> LoadCharacterDataAsync(string path, Action<float> onProgress = null)
        {
            try
            {
                var taskLoadCharacterData = LoadResourceAsync<CharacterData>(path);
                await taskLoadCharacterData;
            
                var characterData = taskLoadCharacterData.Result;
                var taskCharacterIcon = LoadSpriteByURLAsync(characterData.UFXIcon);
                await taskCharacterIcon;

                characterData.Icon = taskCharacterIcon.Result;
                return characterData;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to load Character data at path '{path}': {e.Message}");
                throw;
            }
        }
        
        public async Task TryLoad(Func<Task> load, Func<Task> fallback, Action onSuccess)
        {
            try
            {
                await load();
                onSuccess?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{e.Message}. Loading fallback...");
                await fallback();
                onSuccess?.Invoke();
            }
        }
        
        public async Task TryLoad<T>(Func<Task<T>> load, Func<Task<T>> fallback, Action<T> onSuccess)
        {
            try
            {
                var result = await load();

                if (result == null)
                    throw new NullReferenceException("Asset is null");

                onSuccess?.Invoke(result);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{e.Message}. Loading fallback...");
                onSuccess?.Invoke(await fallback());
            }
        }

    }
}