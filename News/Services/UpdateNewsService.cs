using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using News.Domain.Models;
using News.Interfaces;

namespace News.Services
{
    public class UpdateNewsService : IUpdateNewsService
    {
        private CancellationTokenSource _cts;
        private DateTime _lastDate;

        public event EventHandler<IReadOnlyCollection<NewsModel>> NewsAdded;

        public Task Start(DateTime lastDate)
        {
            _lastDate = lastDate;

            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {

                }
            }, token);
        }

        public void Stop()
        {
            _cts?.Cancel();
        }
    }
}
