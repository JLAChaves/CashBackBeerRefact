using System;
using Bs2.Core2.Utilities.Collections;
using Bs2.Core2.Utilities.Configuration;
using CashBackBeer.Infra.IoC.WorkerDI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace CashBackBeer.WorkerD
{
    class Program
    {
        static void Main(string[] args)
        {

            IConfiguration initialRootSettings = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var _initialRootSettingsKvps = initialRootSettings
                 .ToParsable(canCheckContainsKey: false);

            var reloadedRootSettings = new ConfigurationBuilder()
                        .AddJsonFile("./Config/ConsumerBeerCreated.json", optional: false, reloadOnChange: false)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args)
                        .Build();       

            Enum.TryParse("CONSUMERBEERCREATED", out Workers worker);


            var builder = worker switch
            {
                Workers.CONSUMERBEERCREATED => Host.CreateDefaultBuilder()
                                                       .ConfigureServices((hostContext, services) =>
                                                       {
                                                           services.AddAppWorkersServiceCollection(_initialRootSettingsKvps);
                                                       }).AddConsumerBeerCreated(_initialRootSettingsKvps),

                _ => throw new ArgumentException()
            };


            builder.Build().Run();
        }
    }
}
