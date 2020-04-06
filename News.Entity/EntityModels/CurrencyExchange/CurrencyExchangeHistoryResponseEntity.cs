using System;
using Newtonsoft.Json;

namespace News.Entity.EntityModels.CurrencyExchange
{
    public class CurrencyExchangeHistoryResponseEntity
    {
        [JsonProperty("rates")]
        public CurrencyExchangeHistoryEntity History { get; set; }
    }

    public class CurrencyExchangeHistoryEntity
    {
        public DateTime Date { get; set; }

        [JsonProperty("USD")]
        public float Usd { get; set; }

        [JsonProperty("EUR")]
        public float Eur { get; set; }
    }
}
