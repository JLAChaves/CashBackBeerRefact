using Bs2.Core2.Utilities;
using CashbackBeer.Domain.Pagination;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Domain.Entities.Services
{
    public class FinalSaleDomainService : IFinalSaleDomainService
    {
        private readonly IFinalSaleRepository _finalSaleRepository;
        public FinalSaleDomainService(IFinalSaleRepository finalSaleRepository)
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

        public async ValueTask<ValueResult<IEnumerable<Beer>>> GetBeersByTypeAsync(BeerType beerType, CancellationToken cancellationToken)
        {
            var beersByType = await _finalSaleRepository.GetBeersByTypeAsync(beerType, cancellationToken);
            
            return ValueResult<IEnumerable<Beer>>.Success(beersByType);
        }


        public async ValueTask<ValueResult<Beer>> GetBeerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var beerById = await _finalSaleRepository.GetBeerByIdAsync(id, cancellationToken);

            return ValueResult<Beer>.Success(beerById);
        }

        public async ValueTask<ValueResult<Beer>> GetBeerTypeAndWeekDay(BeerType type, DayOfWeek dayOfWeek, CancellationToken cancellationToken)
        {
            var beerByTypeAndWeekDay = await _finalSaleRepository.GetBeerTypeAndWeekDayAsync(type, dayOfWeek, cancellationToken);

            return ValueResult<Beer>.Success(beerByTypeAndWeekDay);
        }

        public async ValueTask<ValueResult<FinalSale>> CreateFinalSaleAsync(DateTime date, List<PartialSale> partialSales, CancellationToken cancellationToken)
        {
            FinalSale finalSale = new FinalSale(Guid.NewGuid(), partialSales);
            
            await _finalSaleRepository.CreateFinalSaleAsync(finalSale, cancellationToken);

            return ValueResult<FinalSale>.Success(finalSale);
        }

        public async ValueTask<ValueResult<FinalSale>> GetFinalSaleByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var finalSaleById = await _finalSaleRepository.GetFinalSaleByIdAsync(id, cancellationToken);

            return ValueResult<FinalSale>.Success(finalSaleById);
        }

        public async ValueTask<ValueResult<GeneralPagination<FinalSale>>> GetFinalSaleAll(PaginationParams pagination, CancellationToken cancellationToken)
        {
            var finalSaleAll = _finalSaleRepository.GetFinalSaleAll(pagination, cancellationToken);

            return ValueResult<GeneralPagination<FinalSale>>.Success(finalSaleAll);
        }       
    }
}
