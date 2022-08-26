namespace CashBackBeer.Domain.Entities.FinalSaleAggregate
{
    public sealed class PartialSale : EntityImmutable
    {
        public int Amount { get; private set; }
        public decimal ValuePartialSale { get { return Math.Round(Amount * Beer.Value, 2); } }
        public decimal CashbackValue { get { return Math.Round(ValuePartialSale * Beer.CashBack.Percentage, 2); } }
        public Beer Beer { get; set; }

        public PartialSale(int amount, Beer beer)
        {
            Amount = amount;
            Beer = beer;
        }

        public PartialSale()
        {
        }
    }
}