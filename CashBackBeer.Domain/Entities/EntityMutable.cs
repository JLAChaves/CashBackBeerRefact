namespace CashBackBeer.Domain.Entities;

public class EntityMutable : Entity
{
    public DateTimeOffset CreatedInUTC { get; init; }
    public DateTimeOffset? AlteredInUtc { get; set; }

    public EntityMutable()
    {
        CreatedInUTC = DateTimeOffset.UtcNow;
    }
}
