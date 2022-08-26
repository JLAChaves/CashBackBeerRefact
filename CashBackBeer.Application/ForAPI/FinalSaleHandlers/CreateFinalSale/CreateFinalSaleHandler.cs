using CashBackBeer.Application.ForAPI.FinalSaleHandler.CreateFinalSale;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using CashBackBeer.Domain.Entities.Services;
using MediatR;

namespace CashBackBeer.Application.ForAPI.FinalSaleHandlers.CreateFinalSale
{
    public class CreateFinalSaleHandler : IRequestHandler<CreateFinalSaleRequestDTO, CreateFinalSaleResponseDTO>
    {
        private readonly IFinalSaleDomainService _finalSaleDomainService;

        public CreateFinalSaleHandler(IFinalSaleDomainService finalSaleDomainService)
        {
            _finalSaleDomainService = finalSaleDomainService;
        }

        public async Task<CreateFinalSaleResponseDTO> Handle(CreateFinalSaleRequestDTO request, CancellationToken cancellationToken)
        {
            List<PartialSale> partialSales = new List<PartialSale>();

            foreach (var item in request.Items)
            {
                var beer = await _finalSaleDomainService.GetBeerTypeAndWeekDay(
                    item.Type,
                    request.Date.DayOfWeek,
                    cancellationToken);

                if (beer != null)
                    partialSales.Add(new PartialSale(item.Amount, beer.Value));
            }

            if (partialSales.Count > 0)
            {

                var finalSale = new FinalSale(Guid.NewGuid(), partialSales);

                await _finalSaleDomainService.CreateFinalSaleAsync(request.Date, partialSales, cancellationToken);
            }

            CreateFinalSaleResponseDTO createFinalSaleResponseDTO = new CreateFinalSaleResponseDTO();

            return createFinalSaleResponseDTO;
        }

    }
}
