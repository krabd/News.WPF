using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.EntityModels;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using Newtonsoft.Json;

namespace News.DataAccess.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private string BASE_URL = "https://newsapi.org/v2/";

        private string _apiKey;

        public void SetApiKey(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(CancellationToken token)
        {
            return Task.Run<IReadOnlyCollection<NewsModel>>(async () =>
            {
                if (string.IsNullOrEmpty(_apiKey))
                    throw new ArgumentException("ApiKey is empty");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("x-api-key", _apiKey);

                    var endpoint = "top-headlines";

                    var queryParams = new List<string>
                    {
                        "language=" + "ru", 
                        "page=" + 1, 
                        "pageSize=" + 10
                    };

                    var querystring = string.Join("&", queryParams.ToArray());

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + querystring);
                    var httpResponse = await client.SendAsync(httpRequest, token);
                    var json = await httpResponse.Content.ReadAsStringAsync();

                    var news = JsonConvert.DeserializeObject<NewsResponseEntity>(json);

                    return news.News.Select(i => new NewsModel
                    {
                        Author = i.Author,
                        Title = i.Title,
                        Description = i.Description,
                        PublishedDate = i.PublishedDate
                    }).ToList();
                }
            }, token);
        }
    }
}
