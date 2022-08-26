using Bs2.Core2.Utilities;
using CashbackBeer.Domain.Pagination;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using Microsoft.EntityFrameworkCore;

namespace CashBackBeer.Domain.Entities.Services
{
    public interface IFinalSaleDomainService
    {        
        ValueTask<ValueResult<IEnumerable<Beer>>> GetBeersByTypeAsync(            
            BeerType beerType,
            CancellationToken cancellationToken);

        ValueTask<ValueResult<Beer>> GetBeerByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        ValueTask<ValueResult<Beer>> GetBeerTypeAndWeekDay(
            BeerType type, 
            DayOfWeek dayOfWeek, 
            CancellationToken cancellationToken);

        ValueTask<ValueResult<FinalSale>> CreateFinalSaleAsync(
            DateTime date, 
            List<PartialSale> partialSales, 
            CancellationToken cancellationToken);

        ValueTask<ValueResult<FinalSale>> GetFinalSaleByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        ValueTask<ValueResult<GeneralPagination<FinalSale>>> GetFinalSaleAll(
            PaginationParams pagination,
            CancellationToken cancellationToken);
    }
}
