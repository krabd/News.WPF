using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Interfaces;
using News.Utils.Helpers;

namespace News.Services
{
    public class UpdateNewsService : IUpdateNewsService
    {
        private const int REQUEST_NEWS_TIMEOUT_SECONDS = 10;

        private readonly INewsRepository _repository;

        private CancellationTokenSource _cts;
        private DateTime _lastDate;

        public event EventHandler<IReadOnlyCollection<NewsModel>> NewsAdded;

        public UpdateNewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task Start(DateTime lastDate)
        {
            _lastDate = lastDate;

            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            return Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {

                        await Task.Delay(TimeSpan.FromSeconds(REQUEST_NEWS_TIMEOUT_SECONDS), default);
                        token.ThrowIfCancellationRequested();

                        var news = await _repository.GetNewsAsync(_lastDate, default);
                        token.ThrowIfCancellationRequested();

                        if (news.Value == Status.Fail)
                        {
                            Debug.WriteLine(news.Message);
                            continue;
                        }

                        if (!news.Model.Any()) continue;

                        _lastDate = news.Model.OrderByDescending(i => i.PublishedDate).FirstOrDefault()?.PublishedDate ?? DateTime.Now;
                        OnNewsAdded(news.Model);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }, token);
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        private void OnNewsAdded(IReadOnlyCollection<NewsModel> e)
        {
            NewsAdded?.Invoke(this, e);
        }
    }
}
