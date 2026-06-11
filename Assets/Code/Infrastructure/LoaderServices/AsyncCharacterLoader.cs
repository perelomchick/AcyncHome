using System.Threading.Tasks;
using Code.Game;
using Code.ServiceLocator;
using UnityEngine;

namespace Code.Infrastructure.LoaderServices
{
    public class AsyncCharacterLoader : IService
    { 
        private readonly AsyncResourcesLoader  _asyncResourcesLoader;
        private readonly AsyncURLImageLoader _asyncURLImageLoader;
        
        public AsyncCharacterLoader(AsyncResourcesLoader asyncResourcesLoader, AsyncURLImageLoader asyncURLImageLoader)
        {
            _asyncResourcesLoader = asyncResourcesLoader;
            _asyncURLImageLoader = asyncURLImageLoader;
        }
        
        public async Task<CharacterData> LoadCharacterData(string pathToCharacter)
        {
            var taskLoadCharacterData = _asyncResourcesLoader.LoadResources<CharacterData>(pathToCharacter);
            await taskLoadCharacterData;
            
            var characterData = taskLoadCharacterData.Result;
            var taskCharacterIcon = _asyncURLImageLoader.LoadingImageByURL(characterData.UFXIcon);
            await taskCharacterIcon;

            characterData.Icon = taskCharacterIcon.Result;
            return characterData;
        }
    }
}