using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Application.Models
{
    public class PartialSaleDTOAnswer
    {
        public int Amount { get; set; }
        public BeerType Type { get; set; }
        public decimal ValuePartialSale { get; set; }
        public decimal CashBackPercentage { get; set; }
        public decimal CashbackValue { get; set; }
    }
}
