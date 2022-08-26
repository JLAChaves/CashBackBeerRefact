using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashbackBeer.Infra.Data.EntitiesConfiguration
{
    public class PartialSaleConfiguration : IEntityTypeConfiguration<PartialSale>
    {
        public void Configure(EntityTypeBuilder<PartialSale> builder)
        {
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.ValuePartialSale);            
            builder.Property(p => p.Amount);
            //builder.Property(p => p.CashbackValue);
            builder.Property(p => p.CreateInUTC);
            builder.HasOne(p => p.Beer);  
        }
    }
}
