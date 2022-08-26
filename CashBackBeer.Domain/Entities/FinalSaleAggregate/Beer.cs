namespace CashBackBeer.Domain.Entities.FinalSaleAggregate
{
    public sealed class Beer : EntityImmutable
    {
        public BeerType Type { get; init; }
        public decimal Value { get; init; }
        public CashBack CashBack { get; set; }

        private Beer()
        {
        }

        public Beer(Guid id, BeerType type, decimal value, decimal percentage, DayOfWeek dayOfWeek)
        {
            Id = id;
            Type = type;
            Value = value;
            CashBack = new() { Day = dayOfWeek, Percentage = percentage };
        }

        public Beer(BeerType type, decimal value, decimal percentage, DayOfWeek dayOfWeek)
        {
            Type = type;
            Value = value;
            CashBack = new() { Day = dayOfWeek, Percentage = percentage };
        }
    }
}

