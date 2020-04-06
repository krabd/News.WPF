using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using News.DataAccess.Interfaces;
using News.Domain.Models;

namespace News.DataAccess.Repositories
{
    public class NewsRepository : INewsRepository
    {
        public Task<IReadOnlyCollection<NewsModel>> GetNewsAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
