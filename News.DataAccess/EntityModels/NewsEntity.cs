﻿using System;
using Newtonsoft.Json;

namespace News.DataAccess.EntityModels
{
    public class NewsEntity
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime PublishedDate { get; set; }
    }
}
