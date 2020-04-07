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
        private readonly string _baseUrl;

        private readonly HttpClient _client;

        public CurrencyExchangeRepository(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            _client = httpClientFactory.CreateClient();

            _baseUrl = baseUrl;
        }

        public Task<CurrencyExchangeModel> GetCurrencyExchangesAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                var endpoint = "latest";

                var queryParams = new List<string>
                {
                    "base=" + "RUB",
                };

                var querystring = string.Join("&", queryParams.ToArray());

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + querystring);
                var httpResponse = await _client.SendAsync(httpRequest, token);
                var json = await httpResponse.Content.ReadAsStringAsync();

                var currencyExchange = JsonConvert.DeserializeObject<CurrencyExchangeResponseEntity>(json);

                return new CurrencyExchangeModel
                {
                    Usd = currencyExchange.Rates.Usd,
                    Eur = currencyExchange.Rates.Eur
                };
            }, token);
        }
    }
}