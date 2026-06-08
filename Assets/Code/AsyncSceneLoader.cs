using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code
{
    public class AsyncSceneLoader
    {
        public async Task LoadScene(string sceneName, Action<float> progressCallback)
        {
            var  nextScene = SceneManager.LoadSceneAsync(sceneName);
            nextScene.allowSceneActivation = false;

            while (nextScene.progress < 0.9f)
            {
                progressCallback?.Invoke(nextScene.progress);
                await Task.Yield();
            }
            
            progressCallback?.Invoke(1f);
            
            await Task.Yield();
            nextScene.allowSceneActivation = true;
        }
    }
}