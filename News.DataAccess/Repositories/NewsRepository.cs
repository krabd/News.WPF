using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Entity.EntityModels.News;
using Newtonsoft.Json;

namespace News.DataAccess.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly INewsQueryParamsFactory _queryParamsFactory;
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public NewsRepository(INewsQueryParamsFactory queryParamsFactory, IHttpClientFactory httpClientFactory, string baseUrl, string apiKey)
        {
            _queryParamsFactory = queryParamsFactory;
            _client = httpClientFactory.CreateClient();
            _baseUrl = baseUrl;

            _client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public Task<NewsResult> GetNewsAsync(CancellationToken token, int page)
        {
            return Task.Run(async () =>
            {
                var endpoint = "everything";

                var queryParams = _queryParamsFactory.Create(1, DateTime.UtcNow.Date); 
                //_queryParamsFactory.Create(1, DateTime.UtcNow.Date, DateTime.UtcNow.AddHours(-6));

                var querystring = string.Join("&", queryParams.ToArray());

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + querystring);
                var httpResponse = await _client.SendAsync(httpRequest, token);
                var json = await httpResponse.Content.ReadAsStringAsync();

                var news = JsonConvert.DeserializeObject<NewsResponseEntity>(json);

                return new NewsResult
                {
                    TotalCount = news.TotalCount,
                    News = news.News.Select(i => new NewsModel
                    {
                        Author = i.Author,
                        Title = i.Title,
                        Description = i.Description,
                        PublishedDate = i.PublishedDate.ToLocalTime()
                    }).ToList()
                };
            }, token);
        }

        public Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(DateTime startDate, CancellationToken token)
        {
            return Task.Run<IReadOnlyCollection<NewsModel>>(async () =>
            {
                var endpoint = "everything";

                var queryParams = _queryParamsFactory.Create(1, startDate.AddSeconds(1));

                var querystring = string.Join("&", queryParams.ToArray());

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + querystring);
                var httpResponse = await _client.SendAsync(httpRequest, token);
                var json = await httpResponse.Content.ReadAsStringAsync();

                var news = JsonConvert.DeserializeObject<NewsResponseEntity>(json);

                return news.News.Select(i => new NewsModel
                {
                    Author = i.Author,
                    Title = i.Title,
                    Description = i.Description,
                    PublishedDate = i.PublishedDate.ToLocalTime()
                }).ToList();
            }, token);
        }
    }
}
