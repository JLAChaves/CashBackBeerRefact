using CashBackBeer.Application.Models;

namespace CashBackBeer.Application.ForAPI.FinalSaleHandlers.GetFinalSaleById
{
    public class GetFinalSaleByIdResponseDTO
    {
        public DateTimeOffset Date { get; set; }
        public List<PartialSaleDTOAnswer> Items { get; set; }
        public decimal TotalSaleValue { get; set; }
        public decimal TotalCashBackPercentage { get; set; }
        public decimal TotalCashbackValue { get; set; }
    }
}
