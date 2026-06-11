using System;
using System.Threading.Tasks;
using Code.ServiceLocator;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.LoaderServices
{
    public class AsyncResourcesLoader : IService
    {
        public async Task<T> LoadResources<T>(string path,Action<float> onProgress = null) where T : Object
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
    }
}