using Bs2.Core2.Utilities.Messaging.Consumers;
using Bs2.Core2.Utilities.Messaging.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace CashBackBeer.Infra.IoC.Shared
{
    public class ScopedDispatcher<TMessage> : IDispatcher<TMessage>
    {
        private readonly IServiceProvider _provider;
        private readonly Func<IServiceProvider, IDispatcher<TMessage>> _createTarget;

        public ScopedDispatcher(IServiceProvider provider, Func<IServiceProvider, IDispatcher<TMessage>> createTarget)
        {
            _provider = provider;
            _createTarget = createTarget;
        }

        public async ValueTask<IDispatchResult> DispatchAsync(ConsumptionEnvelope<TMessage> envelope, CancellationToken ct)
        {
            using var scope = _provider.CreateScope();
            var targetDispatcher = _createTarget(scope.ServiceProvider);
            return await targetDispatcher.DispatchAsync(envelope, ct);
        }

        public void Dispose() { }
    }
}
