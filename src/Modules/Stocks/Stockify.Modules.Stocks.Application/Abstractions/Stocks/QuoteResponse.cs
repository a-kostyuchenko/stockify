namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public sealed record QuoteResponse(
    string Symbol,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Price,
    long Volume,
    DateOnly LatestTradingDay,
    decimal PreviousClose,
    decimal Change,
    string ChangePercent);
