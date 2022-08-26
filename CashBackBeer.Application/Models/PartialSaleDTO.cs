using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Application.Models
{
    public class PartialSaleDTO
    {
        public int Amount { get; set; }
        public BeerType Type { get; set; }            
    }
}
