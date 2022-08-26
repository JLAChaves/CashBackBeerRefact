namespace CashBackBeer.Application.Models
{
    public class FinalSaleDTO
    {
        public DateTime Date { get; set; }
        public List<PartialSaleDTO> Items { get; set; }
    }
}
