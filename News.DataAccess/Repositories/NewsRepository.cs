using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Entity.EntityModels.News;
using News.Utils.Helpers;
using Newtonsoft.Json;

namespace News.DataAccess.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly INewsQueryParamsFactory _queryParamsFactory;
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly int _pageSize;

        public NewsRepository(INewsQueryParamsFactory queryParamsFactory, IHttpClientFactory httpClientFactory, string baseUrl, string apiKey, int pageSize)
        {
            _queryParamsFactory = queryParamsFactory;
            _client = httpClientFactory.CreateClient();
            _baseUrl = baseUrl;
            _pageSize = pageSize;

            _client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public Task<Result<Status, NewsResult>> GetNewsAsync(int page, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var endpoint = "everything";
                    var queryParams = _queryParamsFactory.Create(page, _pageSize, DateTime.UtcNow.Date);
                    //_queryParamsFactory.Create(page, _pageSize, DateTime.UtcNow.Date, DateTime.UtcNow.AddHours(-6));

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + queryParams);
                    var httpResponse = await _client.SendAsync(httpRequest, token);
                    var json = await httpResponse.Content.ReadAsStringAsync();

                    var news = JsonConvert.DeserializeObject<NewsResponseEntity>(json);

                    if (string.Equals(news.Status, "error"))
                        return new Result<Status, NewsResult>(Status.Fail, message: news.Message);

                    return new Result<Status, NewsResult>(Status.Ok, new NewsResult
                    {
                        TotalCount = news.TotalCount,
                        News = news.News.Select(i => new NewsModel
                        {
                            Author = i.Author,
                            Title = i.Title,
                            Description = i.Description,
                            Url = i.Url,
                            PublishedDate = i.PublishedDate.ToLocalTime()
                        }).ToList()
                    });
                }
                catch (Exception e)
                {
                    return new Result<Status, NewsResult>(Status.Fail, message: e.Message);
                }
            }, token);
        }

        public Task<Result<Status, IReadOnlyCollection<NewsModel>>> GetNewsAsync(DateTime startDate, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var endpoint = "everything";
                    var queryParams = _queryParamsFactory.Create(1, _pageSize, startDate.AddSeconds(1));

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint + "?" + queryParams);
                    var httpResponse = await _client.SendAsync(httpRequest, token);
                    var json = await httpResponse.Content.ReadAsStringAsync();

                    var news = JsonConvert.DeserializeObject<NewsResponseEntity>(json);

                    if (string.Equals(news.Status, "error"))
                        return new Result<Status, IReadOnlyCollection<NewsModel>>(Status.Fail, message: news.Message);

                    return new Result<Status, IReadOnlyCollection<NewsModel>>(Status.Ok, news.News.Select(i => new NewsModel
                    {
                        Author = i.Author,
                        Title = i.Title,
                        Description = i.Description,
                        Url = i.Url,
                        PublishedDate = i.PublishedDate.ToLocalTime()
                    }).ToList());
                }
                catch (Exception e)
                {
                    return new Result<Status, IReadOnlyCollection<NewsModel>>(Status.Fail, message: e.Message);
                }
            }, token);
        }
    }
}
