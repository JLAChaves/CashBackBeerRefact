namespace CashBackBeer.Domain.Entities;

public class Entity
{
    public Guid Id { get; init; }

	public Entity()
	{
		Id = Id == Guid.Empty ? Guid.NewGuid() : Id;
	}
}
