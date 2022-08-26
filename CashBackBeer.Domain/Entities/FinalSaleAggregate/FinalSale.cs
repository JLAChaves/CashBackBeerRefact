namespace CashBackBeer.Domain.Entities.FinalSaleAggregate
{
    public sealed class FinalSale : EntityImmutable
    {
        public decimal TotalSaleValue
        {
            get
            {
                return Math.Round(PartialSales.Sum(p => p.ValuePartialSale), 2);
            }
        }
        public decimal TotalCashBackPercentage
        {
            get
            {
                return Math.Round(PartialSales.Sum(p => p.CashbackValue) / TotalSaleValue, 4);
            }
        }
        public decimal TotalCashbackValue
        {
            get
            {
                return Math.Round(PartialSales.Sum(p => p.CashbackValue), 2);
            }
        }
        public List<PartialSale> PartialSales { get; set; }

        public FinalSale(Guid id, List<PartialSale> partialSales)
        {
            Id = id;
            PartialSales = partialSales;
        }

        public FinalSale(List<PartialSale> partialSales)
        {
            PartialSales = partialSales;
        }

        private FinalSale() { }
    }
}