using Newtonsoft.Json;

namespace News.Entity.EntityModels.CurrencyExchange
{
    public class CurrencyExchangeEntity
    {
        [JsonProperty("USD")]
        public float Usd { get; set; }

        [JsonProperty("EUR")]
        public float Eur { get; set; }
    }
}
