using System;
using System.Threading;
using System.Threading.Tasks;
using News.Domain.Models;
using News.Utils.Helpers;

namespace News.DataAccess.Interfaces
{
    public interface ICurrencyExchangeRepository
    {
        Task<Result<Status, CurrencyExchangeModel>> GetCurrencyExchangesAsync(CancellationToken token);

        Task<Result<Status, CurrencyExchangeModel>> GetCurrencyExchangesAsync(DateTime dateTime, CancellationToken token);
    }
}
