using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code
{
    public class AsyncResourcesLoader
    {
        public async Task<T> LoadResources<T>(string path,Action<float> onProgress) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            

            while (!request.isDone)
            {
                onProgress.Invoke(request.progress);
                await Task.Yield();
            }
            Debug.Log(request.asset as TextAsset);
            
            onProgress.Invoke(1f);
            
            return request.asset as T;
        }
    }
}