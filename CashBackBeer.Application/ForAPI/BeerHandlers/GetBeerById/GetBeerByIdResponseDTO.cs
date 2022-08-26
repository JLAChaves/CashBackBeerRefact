using CashBackBeer.Application.Models;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Application.ForAPI.GetBeerById
{
    public class GetBeerByIdResponseDTO
    {
        public BeerType Type { get; set; }
        public decimal Value { get; set; }
        public CashBackDTO CashBackResponseDTO { get; set; }
    }
}
