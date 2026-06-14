using System;
using System.Collections;
using System.Threading;
using Code.Game.TextServices;
using Code.Game.UI;
using Code.Infrastructure.LoaderServices;
using Code.Infrastructure.ServiceLocator;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Game
{
    public class GameBootstrap : MonoBehaviour
    {       
        [SerializeField] private string _pathToCharacterData;
        [SerializeField] private DialogUI _dialogUI;
        
        private CancellationTokenSource _spamDialogToken = new();
        
        private void Awake()
        {
            RegisterServices();
            SubServices(Services.GetService<SpamDetector>(),
                Services.GetService<DialogManager>(),_dialogUI);
            SendFirstMessageCharacter();
        }

        private async void SendFirstMessageCharacter()
        {
            try
            {
                var dialogManager= Services.GetService<DialogManager>();
                await dialogManager.Load(_pathToCharacterData);
                await dialogManager.NextMassageCharacter(_spamDialogToken.Token);
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to load Character data at path '{_pathToCharacterData}': {e.Message}");
            }
        }

        private void SubServices(SpamDetector spamDetector,DialogManager dialogManager,DialogUI dialogUI)
        {
            spamDetector.OnSendMassage += dialogUI.SetMessage;
            dialogUI.OnPressDialogWindow += 
                () => spamDetector.OnClick(dialogManager?.TriggerMessageCharacter,_spamDialogToken);
            
            dialogManager.OnLoadedCharacterData += dialogUI.SetCharacter;
            dialogManager.OnNextMassage += dialogUI.SetMessage;
            dialogUI.OnPressDialogWindow += 
                () => dialogManager.NextMassageCharacter(_spamDialogToken.Token);
        }

        private void RegisterServices()
        {
            _dialogUI.Construct();
            Services.Register(new TextPrinter());
            Services.Register(new SpamDetector());
            Services.Register(new DialogManager(
                Services.GetService<ILoadService>(), 
                Services.GetService<TextPrinter>()));
        }
    }
}