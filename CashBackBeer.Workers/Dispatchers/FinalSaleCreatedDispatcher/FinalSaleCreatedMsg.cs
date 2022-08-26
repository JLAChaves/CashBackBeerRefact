namespace CashBackBeer.Workers.Dispatchers.FinalSaleDispatcher
{
    public class FinalSaleCreatedMsg
    {
        public DateTime Date { get; set; }
        public List<PartialSaleMsg> Items { get; set; }
    }
}
