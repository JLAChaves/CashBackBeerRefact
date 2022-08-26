using Bs2.Core2.Utilities.Collections;
using Bs2.Core2.Utilities.Messaging.Consumers;
using Bs2.Core2.Utilities.Messaging.RabbitMQ.Consumers;
using Bs2.Core2.Utilities.Messaging.RabbitMQ;
using Bs2.Core2.Utilities.Messaging;
using Bs2.Core2.Utilities.Timing;
using CashBackBeer.Application.Models.Msg;
using CashBackBeer.Infra.IoC.Shared;
using CashBackBeer.Workers.Dispatchers.BeerCreatedDispatcher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using BeerCreatedMsg = CashBackBeer.Workers.Dispatchers.BeerCreatedDispatcher.BeerCreatedMsg;
using CashBackBeer.Domain.Entities.Services;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;

namespace CashBackBeer.Infra.IoC.WorkerDI
{
    public static class WorkerConsumerBeerCreated
    {
        public static IHostBuilder AddConsumerBeerCreated(this IHostBuilder hostBuilder, IParsableKeyValuePairs config) =>
       hostBuilder.ConfigureServices((hostContext, services) =>
       {
           services.AddSingleton(p =>
               new ScopedDispatcher<BeerCreatedMsg>(p, sp =>
                   new BeerCreatedDispatcher(sp.GetRequiredService<ICreateBeer>())));

           services.AddHostedService(x =>
               CreateHostedConsumptionHandler(
                   x.GetRequiredService<ScopedDispatcher<BeerCreatedMsg>>(),
                   x.GetRequiredService<ConnectionFactory>(),
                       "MyWorker",
                       "q.core2.beercreated",
                       "q.core2.beercreated",
                   x.GetRequiredService<IClock>(),
                   x.GetRequiredService<ILoggerFactory>(),
                   x.GetRequiredService<IHostApplicationLifetime>().ApplicationStopped));
       });

        private static IHostedConsumptionHandler<BeerCreatedMsg> CreateHostedConsumptionHandler(
        ScopedDispatcher<BeerCreatedMsg> dispatcher,
        ConnectionFactory factory,
        string consumerName,
        string queue,
        string invalidQueue,
        IClock clock,
        ILoggerFactory logger,
        CancellationToken ct)
        {
            using var hostCanceller = new MessagingHostCanceller(handleCtrlBreak: true);

            var retryRuleMatcher = new RetryRuleMatcher(new List<RetryRule>
        {
            new RetryRule("first_rule", new RetryCountRange(0, 3), TimeSpan.FromDays(1))
        });

            return RabbitMQHostedConsumptionHandlers.CreateWithRetryHandling
            (
                factory,
                cf => cf.CreateConnection(consumerName + $" - {Environment.MachineName}"),
                queue,
                100,
                new ExchangeRoutingKeyPair(null, invalidQueue),
                retryRuleMatcher,
                new ExchangeRoutingKeyPair(null, queue),
                new ExchangeRoutingKeyPair(null, invalidQueue),
                dispatcher,
                TimeSpan.Zero,
                clock,
                hostCanceller,
                logger, ct,
                retryOnUnhandledException: true
            );
        }
    }
    
}
