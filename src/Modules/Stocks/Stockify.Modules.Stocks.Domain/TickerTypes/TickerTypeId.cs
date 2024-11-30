using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.TickerTypes;

public record struct TickerTypeId(Guid Value) : IEntityId<TickerTypeId>
{
    public static TickerTypeId Empty => new(Guid.Empty);
    public static TickerTypeId New() => new(Guid.NewGuid());
    public static TickerTypeId From(Guid value) => new(value);
}
