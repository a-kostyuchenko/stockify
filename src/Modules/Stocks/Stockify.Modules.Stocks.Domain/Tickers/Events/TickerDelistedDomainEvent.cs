using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Tickers.Events;

public sealed class TickerDelistedDomainEvent(Symbol symbol) : DomainEvent
{
    public Symbol Symbol { get; init; } = symbol;
}
