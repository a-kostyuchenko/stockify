using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Tickers;

public static class TickerErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Ticker.NotFound",
        "The ticker with the specified identifier was not found.");
    
    public static readonly Error IsNotUnique = Error.Conflict(
        "Ticker.IsNotUnique",
        "The ticker with the specified symbol or CIK already exists.");
}
