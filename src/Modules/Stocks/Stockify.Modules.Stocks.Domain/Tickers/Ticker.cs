using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Domain.Tickers.Events;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Domain.Tickers;

public sealed class Ticker : Entity<Symbol>
{
    private Ticker()
    {
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Cik { get; private set; }
    public bool Active { get; private set; }
    public TickerTypeId TickerTypeId { get; private set; }
    
    public static Ticker Create(
        string symbol,
        string name,
        string description,
        string cik,
        TickerType tickerType)
    {
        return new Ticker
        {
            Id = Symbol.From(symbol),
            Name = name,
            Description = description,
            Cik = cik,
            Active = true,
            TickerTypeId = tickerType.Id
        };
    }
    
    public void Deactivate()
    {
        if (!Active)
        {
            return;
        }
        
        Active = false;
        
        Raise(new TickerDelistedDomainEvent(Id));
    }
}
