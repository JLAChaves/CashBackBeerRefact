namespace CashBackBeer.Application.Models.Msg
{
    public class FinalSaleCreatedMsg
    {
        public DateTime Date { get; set; }
        public List<PartialSaleDTO> Items { get; set; }
    }
}
