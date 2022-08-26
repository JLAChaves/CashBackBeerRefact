using Bs2.Core2.Utilities;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Domain.Entities.Services
{
    public interface ICreateBeer
    {
        ValueTask<ValueResult<Beer>> CreateBeerAsync(
            BeerType beerType,
            decimal value,
            decimal percentage,
            DayOfWeek dayOfWeek,
            CancellationToken cancellationToken);
    }
}
