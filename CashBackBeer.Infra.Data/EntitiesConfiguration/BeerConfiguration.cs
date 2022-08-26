using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashbackBeer.Infra.Data.EntitiesConfiguration
{
    public class BeerConfiguration : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.CreateInUTC);
            builder.OwnsOne(p => p.CashBack);
        }
    }
}
