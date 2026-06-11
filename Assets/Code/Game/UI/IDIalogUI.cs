using Code.ServiceLocator;
using UnityEngine;

namespace Code.Game.UI
{
    public interface IDialogUI : IService
    {
        void SetCharacter(Sprite icon, string nameCharacter);
        void SetMessage(string textMessage);
    }
}