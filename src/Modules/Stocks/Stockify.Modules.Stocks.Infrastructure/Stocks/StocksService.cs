using System.Globalization;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks;

internal sealed class StocksService(IAlphavantageClient stocksClient) : IStocksService, IScoped
{
    public async Task<Result<QuoteResponse>> GetQuoteAsync(
        string ticker,
        CancellationToken cancellationToken = default)
    {
        GlobalQuote? quote = await stocksClient.GetQuoteAsync(ticker);

        if (quote?.Data is null)
        {
            return Result.Failure<QuoteResponse>(StocksErrors.QuoteNotFound);
        }
        
        return new QuoteResponse(
            quote.Data.Ticker,
            decimal.Parse(quote.Data.Open, CultureInfo.InvariantCulture),
            decimal.Parse(quote.Data.High, CultureInfo.InvariantCulture),
            decimal.Parse(quote.Data.Low, CultureInfo.InvariantCulture),
            decimal.Parse(quote.Data.Price, CultureInfo.InvariantCulture),
            long.Parse(quote.Data.Volume, CultureInfo.InvariantCulture),
            DateOnly.Parse(quote.Data.LatestTradingDay, CultureInfo.InvariantCulture),
            decimal.Parse(quote.Data.PreviousClose, CultureInfo.InvariantCulture),
            decimal.Parse(quote.Data.Change, CultureInfo.InvariantCulture),
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
        string ticker,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TimeSeriesIntraday intradayData = await stocksClient.GetStocksDataAsync(ticker);

            return intradayData.TimeSeries.Select(pair =>
                {
                    (string? key, TimeSeriesEntry? value) = pair;
                    return new TimeSeriesResponse(
                        DateTime.Parse(key, CultureInfo.InvariantCulture),
                        decimal.Parse(value.Open, CultureInfo.InvariantCulture),
                        decimal.Parse(value.High, CultureInfo.InvariantCulture),
                        decimal.Parse(value.Low, CultureInfo.InvariantCulture),
                        decimal.Parse(value.Close, CultureInfo.InvariantCulture),
                        long.Parse(value.Volume, CultureInfo.InvariantCulture));
                })
                .ToList();
        }
        catch
        {
            return Result.Failure<List<TimeSeriesResponse>>(StocksErrors.RequestFailed);
        }
    }
}
