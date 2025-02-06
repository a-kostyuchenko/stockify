using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Domain.Subscriptions;

public sealed class TickerSubscription : Entity<TickerSubscriptionId>
{
    private TickerSubscription() : base(TickerSubscriptionId.New())
    {
    }
    
    public Symbol Symbol { get; private set; }
    public StockholderId StockholderId { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public bool Active { get; private set; }
    
    public static Result<TickerSubscription> Create(Ticker ticker, Stockholder stockholder)
    {
        if (!ticker.Active)
        {
            return Result.Failure<TickerSubscription>(TickerErrors.IsDeactivated);
        }
        
        var subscription = new TickerSubscription
        {
            Symbol = ticker.Id,
            StockholderId = stockholder.Id,
            CreatedAtUtc = DateTime.UtcNow,
            Active = true
        };
        
        return subscription;
    }
}
