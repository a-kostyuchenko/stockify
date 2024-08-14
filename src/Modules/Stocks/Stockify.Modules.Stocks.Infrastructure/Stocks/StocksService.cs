using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks;

internal sealed class StocksService(IAlphavantageClient stocksClient) : IStocksService
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
            quote.Data.Open,
            quote.Data.High,
            quote.Data.Low,
            quote.Data.Price,
            quote.Data.Volume,
            quote.Data.LatestTradingDay,
            quote.Data.PreviousClose,
            quote.Data.Change,
            quote.Data.ChangePercent);
    }
}
