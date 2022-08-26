using CashbackBeer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CashBackBeer.Infra.Data.Context
{
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=localhost,1450;Initial Catalog=CashBackDB;Persist Security Info=True;User ID=SA;Password=Numsey#2022";

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(connectionString).EnableSensitiveDataLogging();

            return new ApplicationDbContext(builder.Options);
        }
    }
}
