using MediatR;

namespace CashBackBeer.Application.ForAPI.GetBeerById
{
    public class GetBeerByIdRequestDTO : IRequest<GetBeerByIdResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
