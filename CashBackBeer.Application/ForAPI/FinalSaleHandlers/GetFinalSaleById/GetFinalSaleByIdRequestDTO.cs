using MediatR;

namespace CashBackBeer.Application.ForAPI.FinalSaleHandlers.GetFinalSaleById
{
    public class GetFinalSaleByIdRequestDTO : IRequest<GetFinalSaleByIdResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
