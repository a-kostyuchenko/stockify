namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public sealed record TimeSeriesResponse(
    DateTime Date,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    long Volume);
