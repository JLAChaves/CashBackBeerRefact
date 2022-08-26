using System.Text.Json.Serialization;

namespace CashBackBeer.Domain.Entities.FinalSaleAggregate;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BeerType
{
    SKOL,
    BRAHMA,
    STELLA,
    BOHEMIA
}
