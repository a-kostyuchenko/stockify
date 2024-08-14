namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public sealed record QuoteResponse(
    string Symbol,
    string Open,
    string High,
    string Low,
    string Price,
    string Volume,
    string LatestTradingDay,
    string PreviousClose,
    string Change,
    string ChangePercent);
