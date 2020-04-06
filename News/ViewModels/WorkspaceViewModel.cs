using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using News.CoreModule.ViewModels;
using News.Utils;
using Prism.Commands;

namespace News.ViewModels
{
    public class WorkspaceViewModel : ViewModelBase
    {
        private CancellationTokenSource _cts;

        public NewsViewModel News { get; }

        public CurrencyExchangeViewModel CurrencyExchange { get; }

        public ICommand ShutdownCommand { get; }

        public ICommand LogoutCommand { get; }

        public WorkspaceViewModel(NewsViewModel news, CurrencyExchangeViewModel currencyExchange)
        {
            News = news;
            CurrencyExchange = currencyExchange;

            ShutdownCommand = new DelegateCommand(OnShutdown);
            LogoutCommand = new DelegateCommand(OnLogout);
        }

        public async Task InitializeAsync()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            try
            {
                var newsTask = News.InitializeAsync(token);
                var currencyExchange = CurrencyExchange.InitializeAsync(token);

                await Tools.WhenAll(newsTask, currencyExchange);
            }
            catch (Exception e) when(!token.IsCancellationRequested)
            {
                Debug.WriteLine(e);
            }
        }

        private void OnShutdown()
        {

        }

        private void OnLogout()
        {

        }
    }
}