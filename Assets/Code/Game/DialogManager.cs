using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Game.Data;
using Code.Game.TextServices;
using Code.Infrastructure.LoaderServices;
using Code.Infrastructure.ServiceLocator;

namespace Code.Game
{
    public class DialogManager : IService
    {
        private readonly TextPrinter _textPrinter;
        private readonly ILoadService _loadService;
        
        private int _indexMessage = -1;
        private CancellationTokenSource _currentDialogToken = new();
        private CharacterData _currentCharacterData;  

        public string TriggerMessageCharacter => _currentCharacterData.TriggerMessage;
        
        public Action<CharacterData> OnLoadedCharacterData;
        public Action<string> OnNextMassage;
        
        public DialogManager(ILoadService loadService, TextPrinter textPrinter)
        {
            _loadService = loadService;
            _textPrinter = textPrinter;
        }
        
        public async Task Load(string pathToCharacter)
        {
            var loadCharacter = _loadService.LoadCharacterDataAsync(pathToCharacter);
            await loadCharacter;
            
            _currentCharacterData = loadCharacter.Result;
            
            OnLoadedCharacterData?.Invoke(_currentCharacterData);
        }

        public async Task NextMassageCharacter(CancellationToken spamToken)
        {
            _currentDialogToken.Cancel();
            _currentDialogToken = new();
            spamToken.ThrowIfCancellationRequested();
            if (_indexMessage >= _currentCharacterData.Messages.Length - 1) return;

            await _textPrinter.Print(
                _currentCharacterData.Messages[++_indexMessage],
                x => OnNextMassage?.Invoke(x),
                _currentDialogToken.Token);
        }
    }
}