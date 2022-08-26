using System.Text.Json.Serialization;

namespace CashBackBeer.Workers.Dispatchers.BeerCreatedDispatcher
{
    public class CashBackMsg
    {
        public decimal Percentage { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { get; set; }
    }
}
