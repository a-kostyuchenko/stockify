using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Tickers.Events;

public sealed class TickerDelistedDomainEvent(TickerId tickerId) : DomainEvent
{
    public TickerId TickerId { get; init; } = tickerId;
}
