using Bs2.Core2.Utilities.Messaging.Consumers;
using Bs2.Core2.Utilities.Messaging.Dispatchers;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using CashBackBeer.Domain.Entities.Services;
using System.Text;

namespace CashBackBeer.Workers.Dispatchers.BeerCreatedDispatcher
{
    public class BeerCreatedDispatcher : IDispatcher<BeerCreatedMsg>
    {
        //private readonly IFinalSaleDomainService _finalSaleDomainService;
        private readonly ICreateBeer _createBeer;

        public BeerCreatedDispatcher(ICreateBeer createBeer)
        {
            _createBeer = createBeer;
        }
        public async ValueTask<IDispatchResult> DispatchAsync(ConsumptionEnvelope<BeerCreatedMsg> envelope, CancellationToken ct)
        {
            var serializedMessage = Encoding.UTF8.GetString(envelope.SerializedMessage.Bytes);

            var beer = envelope.DeserializeMessage();

            //await _finalSaleDomainService.CreateBeerAsync(beer.Type, beer.Value, beer.CashBack.Percentage, beer.CashBack.Day, ct);
            await _createBeer.CreateBeerAsync(beer.Type, beer.Value, beer.CashBack.Percentage, beer.CashBack.Day, ct);

            return DispatchResults.Succeeded();
        }

        public void Dispose()
        {
        }
    }
}
