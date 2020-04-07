using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using News.Domain.Models;

namespace News.DataAccess.Interfaces
{
    public interface INewsRepository
    {
        Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(CancellationToken token);

        Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(DateTime startDate, CancellationToken token);
    }
}
