namespace CashBackBeer.Application.Models
{
    public class FinalSaleDTOAnswer
    {
        public DateTimeOffset Date { get; set; }
        public List<PartialSaleDTOAnswer> Items { get; set; }
        public decimal TotalSaleValue { get; set; }
        public decimal TotalCashBackPercentage { get; set; }
        public decimal TotalCashbackValue { get; set; }

    }
}

