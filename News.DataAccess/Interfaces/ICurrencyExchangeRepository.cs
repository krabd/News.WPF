using System.Threading;
using System.Threading.Tasks;
using News.Domain.Models;

namespace News.DataAccess.Interfaces
{
    public interface ICurrencyExchangeRepository
    {
        Task<CurrencyExchangeModel> GetCurrencyExchangesAsync(CancellationToken token);
    }
}
