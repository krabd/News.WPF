using System.Collections.Generic;

namespace News.Domain.Models
{
    public class NewsResult
    {
        public IReadOnlyCollection<NewsModel> News { get; set; }

        public int TotalCount { get; set; }
    }
}
