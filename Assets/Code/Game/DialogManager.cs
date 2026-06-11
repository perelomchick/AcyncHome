using System.Threading;
using System.Threading.Tasks;
using Code.Game.UI;
using Code.Infrastructure;
using Code.Infrastructure.LoaderServices;
using Code.ServiceLocator;
using UnityEngine;

namespace Code.Game
{
    public class DialogManager : IContructer
    {
        private ShowTextGradually _showTextGradually;
        private AsyncCharacterLoader _asyncCharacterLoader;
        private DialogUI _dialogUI;
        
        private int _indexMassage = -1;
        private CharacterData _characterData;
        public void Construct()
        {
            _showTextGradually = Services.GetService<ShowTextGradually>();
            _asyncCharacterLoader = Services.GetService<AsyncCharacterLoader>();
            _dialogUI = Services.GetService<DialogUI>();
        }
        
        public async Task Load(string pathToCharacter)
        {
            var loadCharacter = _asyncCharacterLoader.LoadCharacterData(pathToCharacter);
            await loadCharacter;
            
            _characterData = loadCharacter.Result;
            
            _dialogUI.SetCharacter(_characterData.Icon,_characterData.Name);
        }

        public async Task NextMassageCharacter(CancellationToken token)
        {            
            if(_indexMassage >= _characterData.Messages.Length)
                return;
            
            _indexMassage++;
            await _showTextGradually.PrintAnimText(_characterData.Messages[_indexMassage],x => _dialogUI.SetMessage(x),token);
        }
    }
}