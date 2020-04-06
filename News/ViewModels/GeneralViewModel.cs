using News.CoreModule.ViewModels;

namespace News.ViewModels
{
    public class GeneralViewModel : ViewModelBase
    {
        public NewsViewModel News { get; }

        public CurrencyExchangeViewModel CurrencyExchange { get; }

        public GeneralViewModel(NewsViewModel news, CurrencyExchangeViewModel currencyExchange)
        {
            News = news;
            CurrencyExchange = currencyExchange;
        }
    }
}
