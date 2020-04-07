using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using News.CoreModule.Enums;
using News.CoreModule.Interfaces;
using News.CoreModule.ViewModels;
using News.Utils;
using News.Utils.Helpers;
using Prism.Commands;

namespace News.ViewModels
{
    public class WorkspaceViewModel : ViewModelBase
    {
        private CancellationTokenSource _cts;

        public IWindowService WindowService { get; set; }

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
            if (WindowService.ShowMessage($"Do you really want to shutdown your computer?", "Warning") ?? false)
                ControlPCHelper.ShutdownPC();
        }

        private void OnLogout()
        {
            if (WindowService.ShowMessage($"Do you really want to logout?", "Warning") ?? false)
                ControlPCHelper.LogOut();
        }
    }
}