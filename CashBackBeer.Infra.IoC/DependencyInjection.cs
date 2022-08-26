using Bs2.Core2.Utilities.Messaging.RabbitMQ;
using Bs2.Core2.Utilities.Messaging.RabbitMQ.Producers;
using CashbackBeer.Infra.Data.Context;
using CashbackBeer.Infra.Data.Repositories;
using CashBackBeer.Application.ForAPI.CreateBeer;
using CashBackBeer.Application.Models.Msg;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using CashBackBeer.Domain.Entities.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CashbackBeer.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IFinalSaleRepository, FinalSaleRepository>();
            services.AddScoped<IFinalSaleDomainService, FinalSaleDomainService>();

            services.AddScoped<IRequestHandler<CreateBeerRequestDTO, CreateBeerResponseDTO>, CreateBeerHandler>();

            var myhandlers = AppDomain.CurrentDomain.Load("CashBackBeer.Application");
            services.AddMediatR(myhandlers);

            services.AddSingleton(new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "vh.core2.beer"
            });

            IConnection CreateConnection(ConnectionFactory connectionFactory) => connectionFactory.CreateConnection();

            services.AddSingleton(x => RabbitMQProducers.CreateConnectionPoolBasedProducerAsync<BeerCreatedMsg>(
                factory: x.GetRequiredService<ConnectionFactory>(),
                CreateConnection,
                new ExchangeRoutingKeyPair(
                    "x.core2.beer", "rk.core2.beer"
                ),
                connectionPoolSize: 2,
                channelPoolSize: 6,
                onPoolExhaustedFallbackOnTansient: true,
                x.GetRequiredService<ILoggerFactory>()

            ));

            services.AddSingleton(x => RabbitMQProducers.CreateConnectionPoolBasedProducerAsync<FinalSaleCreatedMsg>(
                factory: x.GetRequiredService<ConnectionFactory>(),
                CreateConnection,
                new ExchangeRoutingKeyPair(
                    "x.core2.finalsale", "rk.core2.finalsale"
                ),
                connectionPoolSize: 2,
                channelPoolSize: 6,
                onPoolExhaustedFallbackOnTansient: true,
                x.GetRequiredService<ILoggerFactory>()

            ));

            return services;
        }
    }
}
