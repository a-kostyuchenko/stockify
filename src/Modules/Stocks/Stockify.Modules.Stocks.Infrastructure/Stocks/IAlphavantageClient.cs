using Refit;
using Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks;

public interface IAlphavantageClient
{
    [Get("/?function=GLOBAL_QUOTE")]
    Task<GlobalQuote?> GetQuoteAsync(string symbol);
    
    [Get("/?function=TIME_SERIES_INTRADAY&interval=5min")]
    Task<TimeSeriesIntraday> GetStocksDataAsync(string symbol);
    
    [Get("/?function=MARKET_STATUS")]
    Task<MarketStatusData> GetGlobalMarketStatusAsync();
}
