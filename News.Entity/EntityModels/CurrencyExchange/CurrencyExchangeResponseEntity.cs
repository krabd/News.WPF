using Newtonsoft.Json;

namespace News.Entity.EntityModels.CurrencyExchange
{
    public class CurrencyExchangeResponseEntity
    {
        [JsonProperty("rates")]
        public CurrencyExchangeEntity Rates { get; set; }
    }
}
