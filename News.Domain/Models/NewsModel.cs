﻿using System;

namespace News.Domain.Models
{
    public class NewsModel
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
