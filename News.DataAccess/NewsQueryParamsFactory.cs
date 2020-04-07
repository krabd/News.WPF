using System;
using System.Collections.Generic;
using News.DataAccess.Interfaces;

namespace News.DataAccess
{
    public class NewsQueryParamsFactory : INewsQueryParamsFactory
    {
        public string Create(int pageSize)
        {
            var queryParams = new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt",
                "pageSize=" + pageSize
            };

            return string.Join("&", queryParams.ToArray());
        }

        public string Create(int page, int pageSize)
        {
            var queryParams = new List<string>
            {
                "q=" + "world", 
                "sortBy=" + "publishedAt",
                "page=" + page,
                "pageSize=" + pageSize
            };
            return string.Join("&", queryParams.ToArray());
        }

        public string Create(int page, int pageSize, DateTime from)
        {
            var queryParams = new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt",
                "page=" + page,
                "pageSize=" + pageSize,
                "from=" + from.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")
            };
            return string.Join("&", queryParams.ToArray());
        }

        public string Create(int page, int pageSize, DateTime from, DateTime to)
        {
            var queryParams = new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt",
                "page=" + page,
                "pageSize=" + pageSize,
                "from=" + from.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss"),
                "to=" + to.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")
            };
            return string.Join("&", queryParams.ToArray());
        }
    }
}
