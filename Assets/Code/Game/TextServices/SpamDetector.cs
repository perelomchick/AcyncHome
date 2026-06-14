using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Infrastructure.ServiceLocator;

namespace Code.Game.TextServices
{
    public class SpamDetector : IService
    {
        private const int MAX_CLICKS = 3;
        private int _clickCount;
        private CancellationTokenSource _clearClickToken = new();

        public Action<string> OnSendMassage;
        
        public void OnClick(string triggerMassage,CancellationTokenSource spamCancelToken)
        {
            _clearClickToken.Cancel();
            _clearClickToken = new CancellationTokenSource();
            ClearClickCount(_clearClickToken.Token);
            _clickCount++;
            
            if (_clickCount >= MAX_CLICKS)
            {
                spamCancelToken.Cancel();
                OnSendMassage?.Invoke(triggerMassage);
            }
        }

        private async Task ClearClickCount(CancellationToken token)
        {
            await Task.Delay(2000, token);
            _clickCount = 0;
        }
    }
}