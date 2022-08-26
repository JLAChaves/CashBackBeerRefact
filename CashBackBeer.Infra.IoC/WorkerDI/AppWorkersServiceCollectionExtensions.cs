using Bs2.Core2.Utilities.Collections;
using Bs2.Core2.Utilities.Timing;
using CashbackBeer.Infra.Data.Context;
using CashbackBeer.Infra.Data.Repositories;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using CashBackBeer.Domain.Entities.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CashBackBeer.Infra.IoC.WorkerDI
{
    public static class AppWorkersServiceCollectionExtensions
    {
        public static void AddAppWorkersServiceCollection(
            this IServiceCollection services, IParsableKeyValuePairs configKvps)
        {
            var connectionString = "Data Source=localhost,1450;Initial Catalog=CashBackDB;Persist Security Info=True;User ID=SA;Password=Numsey#2022;MultipleActiveResultSets=True";

            var dbContextOptionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionString);

            services.AddSingleton(dbContextOptionsBuilder.Options);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IFinalSaleRepository>(x => new FinalSaleRepository(
                    x.GetRequiredService<ApplicationDbContext>()));

            services.AddScoped<ICreateBeer>(x => new CreateBeer
            (
                x.GetRequiredService<IFinalSaleRepository>()
            ));

            services.AddSingleton(new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "vh.core2.beer"
            });

            IConnection CreateConnection(ConnectionFactory connectionFactory) => connectionFactory.CreateConnection();

            var localTimeZoneId = configKvps.TryGetAndParse<string>("LOCALTIMEZONE");
            TimeZoneInfo localTimeZoneInfo;

            if (localTimeZoneId.KeyDeclared is true)
            {
                localTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(localTimeZoneId.EnsureParsedValue());
            }
            else
            {
                var commonLocalTimeZoneIds = new HashSet<string>(new[]
                {
                    "E. South America Standard Time", // Windows
                    "America/Sao_Paulo" // Debian
                });

                localTimeZoneInfo = TimeZoneInfo
                    .GetSystemTimeZones()
                    .First(x => commonLocalTimeZoneIds.Contains(x.Id));
            }

            var utcToBrTimezone = new UtcToOtherTimeZoneConverter(localTimeZoneInfo);
            services.AddSingleton<IClock>(new DateTimeOffsetClock(utcToBrTimezone));
        }
    }
}
