using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashbackBeer.Infra.Data.EntitiesConfiguration
{
    public class FinalSaleConfiguration : IEntityTypeConfiguration<FinalSale>
    {
        public void Configure(EntityTypeBuilder<FinalSale> builder)
        {
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.TotalSaleValue);
            //builder.Property(p => p.TotalCashBackPercentage);
            //builder.Property(p => p.TotalCashbackValue);
            builder.Property(p => p.CreateInUTC);
            builder.HasMany(p => p.PartialSales);
        }
    }
}
