using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.TickerTypes;

public static class TickerTypeErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "TickerType.NotFound",
        "Ticker type with specified identifier was not found");
    
    public static readonly Error CodeIsNotUnique = Error.Problem(
        "TickerType.CodeIsNotUnique",
        "Ticker type code is not unique");
}
