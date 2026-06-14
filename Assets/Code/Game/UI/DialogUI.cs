using System;
using Code.Game.Data;
using Code.Game.TextServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.UI
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private Image _iconCharacter;
        [SerializeField] private TextMeshProUGUI _nameCharacter;
        [SerializeField] private TextMeshProUGUI _messageCharacter;
        [SerializeField] private Button _pressDialogWindow;
        
        private DialogManager _dialogManager;
        private SpamDetector _spamDetector;
        
        public event Action OnPressDialogWindow;
        
        public void Construct()
        {
            _pressDialogWindow.onClick.AddListener(()  => OnPressDialogWindow?.Invoke());
        }

        public void SetCharacter(CharacterData characterData)
        {
            Debug.Log("SetCharacter");
            _iconCharacter.sprite = characterData.Icon;
            _nameCharacter.text = characterData.Name;
        }

        public void SetMessage(string textMessage) =>
            _messageCharacter.text = textMessage;
    }
}