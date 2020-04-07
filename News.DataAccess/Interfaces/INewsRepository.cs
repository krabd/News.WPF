using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using News.Domain.Models;
using News.Utils.Helpers;

namespace News.DataAccess.Interfaces
{
    public interface INewsRepository
    {
        Task<Result<Status, NewsResult>> GetNewsAsync(CancellationToken token, int page);

        Task<Result<Status, IReadOnlyCollection<NewsModel>>> GetNewsAsync(DateTime startDate, CancellationToken token);
    }
}
