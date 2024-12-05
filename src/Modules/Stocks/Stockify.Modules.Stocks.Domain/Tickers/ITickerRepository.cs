namespace Stockify.Modules.Stocks.Domain.Tickers;

public interface ITickerRepository
{
    Task<Ticker?> GetAsync(TickerId id, CancellationToken cancellationToken = default);
    void Insert(Ticker ticker);
}
