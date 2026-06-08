using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Code
{
    public class AsyncURLImageLoader
    {
        public async Task<Sprite> LoadingImageByURL(string url, Action<float> progressCallback)
        {
            using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            request.SendWebRequest();

            while (!request.isDone)
            {
                progressCallback?.Invoke(request.downloadProgress);
                await Task.Yield();
            }
            
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}