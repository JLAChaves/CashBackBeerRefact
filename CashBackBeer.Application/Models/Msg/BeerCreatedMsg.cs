using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Application.Models.Msg
{
    public class BeerCreatedMsg
    {
        public BeerType Type { get; set; }
        public decimal Value { get; set; }
        public CashBack CashBack { get; set; }
    }
}
