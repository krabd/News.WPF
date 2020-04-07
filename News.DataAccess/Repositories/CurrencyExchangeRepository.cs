using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Entity.EntityModels.CurrencyExchange;
using News.Utils.Helpers;
using Newtonsoft.Json;

namespace News.DataAccess.Repositories
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public CurrencyExchangeRepository(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            _client = httpClientFactory.CreateClient();
            _baseUrl = baseUrl;
        }

        public Task<Result<Status, CurrencyExchangeModel>> GetCurrencyExchangesAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var endpoint = "latest";

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + "base=" + "RUB");
                    var httpResponse = await _client.SendAsync(httpRequest, token);
                    var json = await httpResponse.Content.ReadAsStringAsync();

                    var currencyExchange = JsonConvert.DeserializeObject<CurrencyExchangeResponseEntity>(json);

                    return new Result<Status, CurrencyExchangeModel>(Status.Ok, new CurrencyExchangeModel
                    {
                        Usd = currencyExchange.Rates.Usd,
                        Eur = currencyExchange.Rates.Eur
                    });
                }
                catch (Exception e)
                {
                    return new Result<Status, CurrencyExchangeModel>(Status.Fail, message: e.Message);
                }
            }, token);
        }

        public Task<Result<Status, CurrencyExchangeModel>> GetCurrencyExchangesAsync(DateTime dateTime, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var endpoint = dateTime.ToString("yyyy-MM-dd");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + "base=" + "RUB");
                    var httpResponse = await _client.SendAsync(httpRequest, token);
                    var json = await httpResponse.Content.ReadAsStringAsync();

                    var currencyExchange = JsonConvert.DeserializeObject<CurrencyExchangeResponseEntity>(json);

                    return new Result<Status, CurrencyExchangeModel>(Status.Ok, new CurrencyExchangeModel
                    {
                        Usd = currencyExchange.Rates.Usd,
                        Eur = currencyExchange.Rates.Eur
                    });
                }
                catch (Exception e)
                {
                    return new Result<Status, CurrencyExchangeModel>(Status.Fail, message: e.Message);
                }
            }, token);
        }
    }
}