namespace CashbackBeer.Domain.Pagination
{
    public class GeneralPagination<T>
    {
        public int PageAmount { get; set; }
        public T[] Items { get; set; } = new T[] { };

        public GeneralPagination(int pageAmount, T[] items)
        {
            PageAmount = pageAmount;
            Items = items;
        }
    }
}