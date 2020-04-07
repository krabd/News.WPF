using System;
using System.Collections.Generic;
using News.DataAccess.Interfaces;

namespace News.DataAccess
{
    public class NewsQueryParamsFactory : INewsQueryParamsFactory
    {
        public string Create()
        {
            var queryParams = new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt"
            };

            return string.Join("&", queryParams.ToArray());
        }

        public string Create(int page)
        {
            var queryParams = new List<string>
            {
                "q=" + "world", 
                "sortBy=" + "publishedAt",
                "page=" + page
            };
            return string.Join("&", queryParams.ToArray());
        }

        public string Create(int page, DateTime from)
        {
            var queryParams = new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt",
                "page=" + page,
                "from=" + from.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")
            };
            return string.Join("&", queryParams.ToArray());
        }

        public string Create(int page, DateTime from, DateTime to)
        {
            var queryParams = new List<string>
            {
                "q=" + "world",
                "sortBy=" + "publishedAt",
                "page=" + page,
                "from=" + from.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss"),
                "to=" + to.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")
            };
            return string.Join("&", queryParams.ToArray());
        }
    }
}
