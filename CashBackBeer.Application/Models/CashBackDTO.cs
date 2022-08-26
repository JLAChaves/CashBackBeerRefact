using System.Text.Json.Serialization;

namespace CashBackBeer.Application.Models
{
    public class CashBackDTO
    {
        public decimal Percentage { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { get; set; }
    }
}
