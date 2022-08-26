using CashBackBeer.Application.Models;
using CashBackBeer.Domain.Entities.Services;
using MediatR;

namespace CashBackBeer.Application.ForAPI.GetBeerById
{
    public class GetBeerByIdHandler : IRequestHandler<GetBeerByIdRequestDTO, GetBeerByIdResponseDTO>
    {
        private readonly IFinalSaleDomainService _finalSaleDomainService;

        public GetBeerByIdHandler(IFinalSaleDomainService finalSaleDomainService)
        {
            _finalSaleDomainService = finalSaleDomainService;
        }
        public async Task<GetBeerByIdResponseDTO> Handle(GetBeerByIdRequestDTO request, CancellationToken cancellationToken)
        {
            var beerById = await _finalSaleDomainService.GetBeerByIdAsync(request.Id, cancellationToken);

            GetBeerByIdResponseDTO getBeerByIdResponseDTO = new GetBeerByIdResponseDTO()
            {
                Type = beerById.Value.Type,
                Value = beerById.Value.Value,
                CashBackResponseDTO = new CashBackDTO()
                {
                    Percentage = beerById.Value.CashBack.Percentage,
                    Day = beerById.Value.CashBack.Day,
                }
            };

            return getBeerByIdResponseDTO;
        }
    }
}
