using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using News.Domain.Models;

namespace News.DataAccess.Interfaces
{
    public interface INewsRepository
    {
        Task<NewsResult> GetNewsAsync(CancellationToken token, int page);

        Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(DateTime startDate, CancellationToken token);
    }
}
