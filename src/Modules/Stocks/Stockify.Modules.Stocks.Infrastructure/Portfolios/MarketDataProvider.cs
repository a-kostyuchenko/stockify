using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Portfolios;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Domain.Portfolios;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Infrastructure.Portfolios;

internal sealed class MarketDataProvider(
    IStocksService stocksService) : IMarketDataProvider, IScoped
{
    public async Task<IReadOnlyList<AssetMetrics>> GetAssetsMetricsAsync(
        IEnumerable<Symbol> tickerIds,
        CancellationToken cancellationToken = default)
    {
        var symbols = tickerIds.ToList();
        
        var result = new List<AssetMetrics>();
        var timeSeriesData = new Dictionary<Symbol, List<TimeSeriesResponse>>();
        
        foreach (Symbol symbol in symbols)
        {
            Result<List<TimeSeriesResponse>> timeSeriesResult = await stocksService.GetStocksData(
                symbol.Value,
                cancellationToken);

            if (timeSeriesResult.IsSuccess)
            {
                timeSeriesData[symbol] = timeSeriesResult.Value;
            }
        }
        
        foreach (Symbol symbol in symbols)
        {
            if (!timeSeriesData.TryGetValue(symbol, out List<TimeSeriesResponse>? value))
            {
                continue;
            }

            List<decimal> returns = CalculateReturns(value);
            decimal expectedReturn = returns.Average();
            decimal volatility = CalculateVolatility(returns);
            Dictionary<Symbol, decimal> correlations = CalculateCorrelations(symbol, timeSeriesData);

            result.Add(new AssetMetrics(
                symbol,
                expectedReturn,
                volatility,
                correlations));
        }

        return result;
    }
    
    // TODO: Extract calculations to separate class
    private static List<decimal> CalculateReturns(List<TimeSeriesResponse> timeSeries)
    {
        var returns = new List<decimal>();
        for (int i = 1; i < timeSeries.Count; i++)
        {
            decimal currentPrice = timeSeries[i].Close;
            decimal previousPrice = timeSeries[i - 1].Close;
            decimal @return = (currentPrice - previousPrice) / previousPrice;
            returns.Add(@return);
        }
        return returns;
    }

    private static decimal CalculateVolatility(List<decimal> returns)
    {
        decimal mean = returns.Average();
        decimal sumSquaredDeviations = returns.Sum(r => (r - mean) * (r - mean));
        decimal variance = sumSquaredDeviations / (returns.Count - 1);
        return (decimal)Math.Sqrt((double)variance);
    }

    private static Dictionary<Symbol, decimal> CalculateCorrelations(
        Symbol baseTicker,
        Dictionary<Symbol, List<TimeSeriesResponse>> timeSeriesData)
    {
        var correlations = new Dictionary<Symbol, decimal>();
        List<decimal> baseReturns = CalculateReturns(timeSeriesData[baseTicker]);

        foreach ((Symbol otherTicker, List<TimeSeriesResponse> otherTimeSeries) in timeSeriesData)
        {
            if (otherTicker == baseTicker)
            {
                correlations[otherTicker] = 1m;
                continue;
            }

            List<decimal> otherReturns = CalculateReturns(otherTimeSeries);
            decimal correlation = CalculateCorrelation(baseReturns, otherReturns);
            correlations[otherTicker] = correlation;
        }

        return correlations;
    }

    private static decimal CalculateCorrelation(List<decimal> x, List<decimal> y)
    {
        int n = Math.Min(x.Count, y.Count);
        decimal meanX = x.Take(n).Average();
        decimal meanY = y.Take(n).Average();

        decimal sumXY = 0m;
        decimal sumX2 = 0m;
        decimal sumY2 = 0m;

        for (int i = 0; i < n; i++)
        {
            decimal dx = x[i] - meanX;
            decimal dy = y[i] - meanY;
            sumXY += dx * dy;
            sumX2 += dx * dx;
            sumY2 += dy * dy;
        }

        return sumXY / (decimal)Math.Sqrt((double)(sumX2 * sumY2));
    }
}
