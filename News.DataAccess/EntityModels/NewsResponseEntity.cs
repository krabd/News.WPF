﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace News.DataAccess.EntityModels
{
    public class NewsResponseEntity
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalResults")]
        public int TotalCount { get; set; }

        [JsonProperty("articles")]
        public IReadOnlyCollection<NewsEntity> News { get; set; }
    }
}
