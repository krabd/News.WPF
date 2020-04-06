using System.Threading;
using System.Threading.Tasks;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;

namespace News.ViewModels
{
    public class CurrencyExchangeViewModel : ViewModelBase
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;
        private float _usd;
        private float _eur;

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
            var currencyExchanges = await _currencyExchangeRepository.GetCurrencyExchangesAsync(token);
            token.ThrowIfCancellationRequested();

            Usd = 1 / currencyExchanges.Usd;
            Eur = 1 / currencyExchanges.Eur;
        }
    }
}
