using System.Threading;
using Code.Game.UI;
using Code.Infrastructure.LoaderServices;
using Code.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game
{
    public class BootstrapGame : MonoBehaviour
    {       
        [SerializeField] private string _pathToCharacter;
        [SerializeField] private DialogUI _dialogUI;
        [SerializeField] private Button _pressToDialogWindow;

        private DialogManager _dialogManager;
        private CancellationTokenSource _currentDialogToken = new();
        private SpamDetectorOnSkip  _spamDetectorOnSkip;
        private bool isManySkip = false;
        
        private async void Awake()
        {
            RegisterServices();

            _spamDetectorOnSkip = new();
            _spamDetectorOnSkip.Construct();
            
            _dialogManager = new DialogManager();
            _dialogManager.Construct();
            await _dialogManager.Load(_pathToCharacter);
            _dialogManager.NextMassageCharacter(_currentDialogToken.Token);
            
            _pressToDialogWindow.onClick.AddListener(SkipMessage);
            _pressToDialogWindow.onClick.AddListener(() => isManySkip = _spamDetectorOnSkip.OnSkip());
        }

        private void SkipMessage()
        {                
            _currentDialogToken?.Cancel();
            
            if (isManySkip)
                return;
            
            _currentDialogToken = new();
            _dialogManager.NextMassageCharacter(_currentDialogToken.Token);
        }

        private void RegisterServices()
        {
            Services.Register(new ShowTextGradually());
            Services.Register(new AsyncCharacterLoader(
                Services.GetService<AsyncResourcesLoader>(),
                Services.GetService<AsyncURLImageLoader>()));
            
            Services.Register(_dialogUI);
        }
    }
}