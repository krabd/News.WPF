using System;
using System.Collections.Generic;
using News.DataAccess.Interfaces;

namespace News.DataAccess
{
    public class NewsQueryParamsFactory : INewsQueryParamsFactory
    {
        public List<string> Create()
        {
            return new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt"
            };
        }

        public List<string> Create(int page)
        {
            var queryParams = Create();
            queryParams.Add("page=" + page);
            return queryParams;
        }

        public List<string> Create(int page, DateTime from)
        {
            var queryParams = Create(page);
            queryParams.Add("from=" + from.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss"));
            return queryParams;
        }

        public List<string> Create(int page, DateTime from, DateTime to)
        {
            var queryParams = Create(page, from);
            queryParams.Add("to=" + to.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss"));
            return queryParams;
        }
    }
}
