using System;

namespace News.Domain.Models
{
    public class NewsModel
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
