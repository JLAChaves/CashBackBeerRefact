using CashBackBeer.Application.Models;
using MediatR;

namespace CashBackBeer.Application.ForAPI.FinalSaleHandler.CreateFinalSale
{
    public class CreateFinalSaleRequestDTO : IRequest<CreateFinalSaleResponseDTO>
    {
        public DateTime Date { get; set; }
        public List<PartialSaleDTO> Items { get; set; }
    }
}
