using System.Text.Json.Serialization;

namespace CashBackBeer.Domain.Entities.FinalSaleAggregate
{
    public sealed class CashBack
    {
        public decimal Percentage { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { get; init; }
    }
}
