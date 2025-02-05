using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Stockholders;

public static class StockholderErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Stockholder.NotFound",
        "The stockholder with the specified identifier was not found.");
}
