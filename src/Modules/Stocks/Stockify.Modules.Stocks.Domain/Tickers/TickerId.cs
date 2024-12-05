using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Tickers;

public record struct TickerId(Guid Value) : IEntityId<TickerId>
{
    public static TickerId Empty => new(Guid.Empty);
    public static TickerId New() => new(Guid.NewGuid());
    public static TickerId From(Guid value) => new(value);
}
