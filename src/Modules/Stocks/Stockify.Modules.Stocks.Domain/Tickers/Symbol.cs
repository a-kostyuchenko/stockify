using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Tickers;

public readonly record struct Symbol(string Value) : IEntityId<Symbol, string>
{
    public static Symbol Empty => new(string.Empty);
    public static Symbol New() => new(Guid.NewGuid().ToString("N"));

    public static Symbol From(string value) => new(value);
}
