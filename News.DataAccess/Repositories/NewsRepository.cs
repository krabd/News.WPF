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
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public NewsRepository(IHttpClientFactory httpClientFactory, string baseUrl, string apiKey)
        {
            _client = httpClientFactory.CreateClient();
            _baseUrl = baseUrl;

            _client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(CancellationToken token)
        {
            return Task.Run<IReadOnlyCollection<NewsModel>>(async () =>
            {
                var endpoint = "top-headlines";

                var queryParams = new List<string>
                {
                    "language=" + "ru",
                    "page=" + 1,
                    "pageSize=" + 10
                };

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
                    PublishedDate = i.PublishedDate
                }).ToList();
            }, token);
        }
    }
}
