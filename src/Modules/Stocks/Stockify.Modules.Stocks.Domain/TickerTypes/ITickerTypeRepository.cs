namespace Stockify.Modules.Stocks.Domain.TickerTypes;

public interface ITickerTypeRepository
{
    Task<TickerType?> GetAsync(TickerTypeId tickerTypeId, CancellationToken cancellationToken = default);
    void Insert(TickerType tickerType);
}
