using Code.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.UI
{
    public class DialogUI : MonoBehaviour, IDialogUI
    {
        [SerializeField] private Image _iconCharacter;
        [SerializeField] private TextMeshProUGUI _nameCharacter;
        [SerializeField] private TextMeshProUGUI _messageCharacter;

        public void SetCharacter(Sprite icon, string nameCharacter)
        {
            _iconCharacter.sprite = icon;
            _nameCharacter.text = nameCharacter;
        }

        public void SetMessage(string textMessage) =>
            _messageCharacter.text = textMessage;
    }
}