using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Application.Models
{
    public class BeerDTO
    {
        public BeerType Type { get; set; }
        public decimal Value { get; set; }
        public CashBackDTO CashBackDTO { get; set; }
    }
}
