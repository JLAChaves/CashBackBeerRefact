using CashBackBeer.Domain.Entities.Services;
using MediatR;

namespace CashBackBeer.Application.ForAPI.CreateBeer
{
    public class CreateBeerHandler : IRequestHandler<CreateBeerRequestDTO, CreateBeerResponseDTO>
    {
        private readonly ICreateBeer _createBeer;

        public CreateBeerHandler(ICreateBeer createBeer)
        {
            _createBeer = createBeer;
        }

        public async Task<CreateBeerResponseDTO> Handle(CreateBeerRequestDTO request, CancellationToken cancellationToken)
        {
            await _createBeer.CreateBeerAsync(
                request.Type,
                request.Value,
                request.CashBackRequestDTO.Percentage,
                request.CashBackRequestDTO.Day,
                cancellationToken);

            CreateBeerResponseDTO createBeerResponseDTO = new CreateBeerResponseDTO();

            return createBeerResponseDTO;
        }
    }
}
