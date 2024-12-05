using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Tickers;

public static class TickerErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Ticker.NotFound",
        "The ticker with the specified identifier was not found.");
}
