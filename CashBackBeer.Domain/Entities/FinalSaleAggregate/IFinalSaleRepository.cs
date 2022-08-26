using CashbackBeer.Domain.Pagination;

namespace CashBackBeer.Domain.Entities.FinalSaleAggregate
{
    public interface IFinalSaleRepository
    {
        GeneralPagination<FinalSale> GetFinalSaleAll(PaginationParams pagination, CancellationToken cancellationToken);
        Task<FinalSale> GetFinalSaleByIdAsync(Guid id, CancellationToken cancellationToken);
        //GeneralPagination<FinalSale> GetFinalSaleByDate(PaginationParams pagination, DateTime? minDate, DateTime? maxDate);
        Task<FinalSale> CreateFinalSaleAsync(FinalSale finalSale, CancellationToken cancellationToken);
        Task<IEnumerable<Beer>> GetBeersAsync();
        Task<IEnumerable<Beer>> GetBeersByTypeAsync(BeerType beerType, CancellationToken cancellationToken);
        Task<Beer> GetBeerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Beer> CreateBeerAsync(Beer beer, CancellationToken cancellationToken);
        Task<Beer> UpdateBeerAsync(Beer beer, CancellationToken cancellationToken);
        Task<Beer> RemoveBeerAsync(Beer beer, CancellationToken cancellationToken);
        Task<Beer> GetBeerTypeAndWeekDayAsync(BeerType type, DayOfWeek dayOfWeek, CancellationToken cancellationToken);
    }
}