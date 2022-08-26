using CashBackBeer.Application.Models;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using MediatR;

namespace CashBackBeer.Application.ForAPI.CreateBeer
{
    public class CreateBeerRequestDTO : IRequest<CreateBeerResponseDTO>
    {
        public BeerType Type { get; set; }
        public decimal Value { get; set; }
        public CashBackDTO CashBackRequestDTO { get; set; }
    }
}
