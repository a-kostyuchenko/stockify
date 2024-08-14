using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public static class StocksErrors
{
    public static readonly Error QuoteNotFound = Error.NotFound(
        "Stocks.QuoteNotFound",
        "The requested quote was not found.");
}
