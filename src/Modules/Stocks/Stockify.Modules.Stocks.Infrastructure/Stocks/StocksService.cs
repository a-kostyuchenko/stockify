using System.Globalization;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks;

internal sealed class StocksService(IAlphavantageClient stocksClient) : IStocksService, IScoped
{
    public async Task<Result<QuoteResponse>> GetQuoteAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        GlobalQuote? quote = await stocksClient.GetQuoteAsync(symbol);

        if (quote?.Data is null)
        {
            return Result.Failure<QuoteResponse>(StocksErrors.QuoteNotFound);
        }
        
        return new QuoteResponse(
            quote.Data.Symbol,
            decimal.Parse(quote.Data.Open, CultureInfo.CurrentCulture),
            decimal.Parse(quote.Data.High, CultureInfo.CurrentCulture),
            decimal.Parse(quote.Data.Low, CultureInfo.CurrentCulture),
            decimal.Parse(quote.Data.Price, CultureInfo.CurrentCulture),
            long.Parse(quote.Data.Volume, CultureInfo.CurrentCulture),
            DateOnly.Parse(quote.Data.LatestTradingDay, CultureInfo.CurrentCulture),
            decimal.Parse(quote.Data.PreviousClose, CultureInfo.CurrentCulture),
            decimal.Parse(quote.Data.Change, CultureInfo.CurrentCulture),
            quote.Data.ChangePercent);
    }

    public async Task<Result<List<MarketResponse>>> GetGlobalMarketStatusAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            MarketStatusData marketStatuses = await stocksClient.GetGlobalMarketStatusAsync();

            return marketStatuses.Markets
                .Select(m => new MarketResponse(
                    m.MarketType,
                    m.Region,
                    m.PrimaryExchanges,
                    m.LocalOpen,
                    m.LocalClose,
                    m.CurrentStatus,
                    m.Notes))
                .ToList();
        }
        catch
        {
            return Result.Failure<List<MarketResponse>>(StocksErrors.RequestFailed);
        }
    }

    public async Task<Result<List<TimeSeriesResponse>>> GetStocksData(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TimeSeriesIntraday intradayData = await stocksClient.GetStocksDataAsync(symbol);

            return intradayData.TimeSeries.Select(pair =>
                {
                    (string? key, TimeSeriesData? value) = pair;
                    return new TimeSeriesResponse(
                        DateTime.Parse(key, CultureInfo.CurrentCulture),
                        decimal.Parse(value.Open, CultureInfo.CurrentCulture),
                        decimal.Parse(value.High, CultureInfo.CurrentCulture),
                        decimal.Parse(value.Low, CultureInfo.CurrentCulture),
                        decimal.Parse(value.Close, CultureInfo.CurrentCulture),
                        long.Parse(value.Volume, CultureInfo.CurrentCulture));
                })
                .ToList();
        }
        catch
        {
            return Result.Failure<List<TimeSeriesResponse>>(StocksErrors.RequestFailed);
        }
    }
}
