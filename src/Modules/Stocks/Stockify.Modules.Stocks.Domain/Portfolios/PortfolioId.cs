using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public readonly record struct PortfolioId(Guid Value) : IEntityId<PortfolioId>
{
    public static PortfolioId Empty => new(Guid.Empty);
    public static PortfolioId New() => new(Guid.NewGuid());
    public static PortfolioId From(Guid value) => new(value);
}