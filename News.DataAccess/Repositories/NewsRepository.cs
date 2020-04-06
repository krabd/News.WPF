using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;

namespace News.DataAccess.Repositories
{
    public class NewsRepository : INewsRepository
    {
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

                var url = $"http://newsapi.org/v2/top-headlines?country=us&apiKey={_apiKey}";

                using (var client = new WebClient())
                {
                    var json = await client.DownloadStringTaskAsync(new Uri(url));
                    var a = 1;
                }

                throw new NotImplementedException();
            }, token);
        }
    }
}
