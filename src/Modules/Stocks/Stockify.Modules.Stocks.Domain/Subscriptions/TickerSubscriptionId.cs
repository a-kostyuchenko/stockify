using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Subscriptions;

public record struct TickerSubscriptionId(Guid Value) : IEntityId<TickerSubscriptionId>
{
    public static TickerSubscriptionId Empty => new(Guid.Empty);
    public static TickerSubscriptionId New() => new(Guid.NewGuid());

    public static TickerSubscriptionId From(Guid value) => new(value);
}
