using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;
using News.Utils;
using News.Utils.Helpers;
using OxyPlot;

namespace News.ViewModels
{
    public class CurrencyExchangeViewModel : ViewModelBase
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        private float _usd;
        private float _eur;

        public ObservableCollection<DataPoint> UsdHistory { get; } = new ObservableCollection<DataPoint>();

        public ObservableCollection<DataPoint> EurHistory { get; } = new ObservableCollection<DataPoint>();

        public float Usd
        {
            get => _usd;
            set => SetProperty(ref _usd, value);
        }

        public float Eur
        {
            get => _eur;
            set => SetProperty(ref _eur, value);
        }

        public CurrencyExchangeViewModel(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            var currencyExchangesTask = _currencyExchangeRepository.GetCurrencyExchangesAsync(token);
            var historyTasks = Enumerable.Range(0, 12).Select(i => _currencyExchangeRepository.GetCurrencyExchangesAsync(DateTime.UtcNow.AddMonths(-(12 - i)).Date, token)).ToList();
            
            await Tools.WhenAll(historyTasks, currencyExchangesTask);
            token.ThrowIfCancellationRequested();

            var currencyExchanges = currencyExchangesTask.Result;
            if (currencyExchanges.Value == Status.Fail)
            {
                Debug.WriteLine(currencyExchanges.Message);
            }
            else
            {
                Usd = 1 / currencyExchanges.Model.Usd;
                Eur = 1 / currencyExchanges.Model.Eur;
            }

            Tools.DispatchedInvoke(() =>
            {
                UsdHistory.Clear();
                EurHistory.Clear();

                UsdHistory.AddRange(historyTasks.Select((v, i) => new DataPoint(i + 1, 1 / v.Result.Model.Usd)));
                EurHistory.AddRange(historyTasks.Select((v, i) => new DataPoint(i + 1, 1 / v.Result.Model.Eur)));
            });
        }
    }
}
