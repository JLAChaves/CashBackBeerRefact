using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Workers.Dispatchers.FinalSaleDispatcher
{
    public class PartialSaleMsg
    {
        public int Amount { get; set; }
        public BeerType Type { get; set; }
    }
}
