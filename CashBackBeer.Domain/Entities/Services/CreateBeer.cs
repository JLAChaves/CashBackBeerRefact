using Bs2.Core2.Utilities;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Domain.Entities.Services
{
    public class CreateBeer : ICreateBeer
    {
        private readonly IFinalSaleRepository _finalSaleRepository;
        public CreateBeer(IFinalSaleRepository finalSaleRepository)
        {
            _finalSaleRepository = finalSaleRepository;
        }

        public async ValueTask<ValueResult<Beer>> CreateBeerAsync(
            BeerType beerType,
            decimal value,
            decimal percentage,
            DayOfWeek dayOfWeek,
            CancellationToken cancellationToken)
        {
            var beer = new Beer(beerType, value, percentage, dayOfWeek);

            await _finalSaleRepository.CreateBeerAsync(beer, cancellationToken);

            return ValueResult<Beer>.Success(beer);
        }
    }
}
