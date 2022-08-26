namespace CashBackBeer.Domain.Entities;

public class EntityImmutable : Entity
{
    public DateTimeOffset CreateInUTC { get; set; }

	public EntityImmutable()
	{
		CreateInUTC = DateTimeOffset.UtcNow;
	}
}
