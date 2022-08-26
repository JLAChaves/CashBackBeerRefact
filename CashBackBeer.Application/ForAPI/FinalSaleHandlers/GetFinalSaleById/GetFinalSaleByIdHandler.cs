using CashBackBeer.Application.Models;
using CashBackBeer.Domain.Entities.Services;
using MediatR;

namespace CashBackBeer.Application.ForAPI.FinalSaleHandlers.GetFinalSaleById
{
    public class GetFinalSaleByIdHandler : IRequestHandler<GetFinalSaleByIdRequestDTO, GetFinalSaleByIdResponseDTO>
    {
        private readonly IFinalSaleDomainService _finalSaleDomainService;

        public GetFinalSaleByIdHandler(IFinalSaleDomainService finalSaleDomainService)
        {
            _finalSaleDomainService = finalSaleDomainService;
        }
        public async Task<GetFinalSaleByIdResponseDTO> Handle(GetFinalSaleByIdRequestDTO request, CancellationToken cancellationToken)
        {
            var finalSaleById = await _finalSaleDomainService.GetFinalSaleByIdAsync(
                request.Id, 
                cancellationToken);

            List<PartialSaleDTOAnswer> answers = new List<PartialSaleDTOAnswer>();

            foreach (var item in finalSaleById.Value.PartialSales)
            {
                PartialSaleDTOAnswer partial = new PartialSaleDTOAnswer()
                {
                    Amount = item.Amount,
                    Type = item.Beer.Type,
                    CashBackPercentage = item.Beer.CashBack.Percentage,
                    CashbackValue = item.CashbackValue,
                    ValuePartialSale = item.ValuePartialSale
                };
                answers.Add(partial);
            }

            GetFinalSaleByIdResponseDTO response = new GetFinalSaleByIdResponseDTO()
            {
                Date = finalSaleById.Value.CreateInUTC,
                Items = answers,
                TotalCashBackPercentage = finalSaleById.Value.TotalCashBackPercentage,
                TotalCashbackValue = finalSaleById.Value.TotalCashbackValue,
                TotalSaleValue = finalSaleById.Value.TotalSaleValue
            };

            return response;
        }
    }
}
