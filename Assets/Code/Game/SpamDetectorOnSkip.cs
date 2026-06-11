using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Game.UI;
using Code.Infrastructure;
using Code.ServiceLocator;

namespace Code.Game
{
    public class SpamDetectorOnSkip : IContructer
    {
        private const int MAX_CLICKS = 3;
        private int _clickCount;
        
        private DialogUI  _dialogUI;
        
        public void Construct()
        {
            _dialogUI = Services.GetService<DialogUI>();
        }
        
        public bool OnSkip( )
        {
            _clickCount++;

            if (_clickCount >= MAX_CLICKS)
            {
                ShowMessage("Молодец, ты перескипол кота. Теперь сиди тут и думай!");

                return true;
            }
            
            return false;
        }

        private void ShowMessage(string message)
        {
            _dialogUI.SetMessage(message);
        }
    }
}