using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Entity.EntityModels.CurrencyExchange;
using Newtonsoft.Json;

namespace News.DataAccess.Repositories
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private string BASE_URL = "https://api.exchangeratesapi.io/";

        public Task<CurrencyExchangeModel> GetCurrencyExchangesAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {
                    var endpoint = "latest";

                    var queryParams = new List<string>
                    {
                        "base=" + "RUB",
                    };

                    var querystring = string.Join("&", queryParams.ToArray());

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + querystring);
                    var httpResponse = await client.SendAsync(httpRequest, token);
                    var json = await httpResponse.Content.ReadAsStringAsync();

                    var currencyExchange = JsonConvert.DeserializeObject<CurrencyExchangeResponseEntity>(json);

                    return new CurrencyExchangeModel
                    {
                        Usd = currencyExchange.Rates.Usd,
                        Eur = currencyExchange.Rates.Eur
                    };
                }
            }, token);
        }
    }
}