using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using News.Domain.Models;

namespace News.Interfaces
{
    public interface IUpdateNewsService
    {
        event EventHandler<IReadOnlyCollection<NewsModel>> NewsAdded;

        Task Start(DateTime lastDate);

        void Stop();
    }
}
