using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Code.ServiceLocator;

namespace Code.Game
{
    public class ShowTextGradually : IService
    {
        private const int DELAY_SYMBOL = 80;
        private const int DELAY_PUNCTUAL_SYMBOL = 160;
        
        private readonly List<char> _punctual_symbols = new() { '.', ',', '!' };
        
        public async Task PrintAnimText(string text, Action<string> callback,CancellationToken token)
        {
            var showText = "";
            
            foreach (var symbol in text)
            {
                token.ThrowIfCancellationRequested();
                showText += symbol;
                callback.Invoke(showText);
                
                if(_punctual_symbols.Contains(symbol))
                    await Task.Delay(DELAY_SYMBOL);
                else
                    await Task.Delay(DELAY_PUNCTUAL_SYMBOL);
            }
        }
    }
}