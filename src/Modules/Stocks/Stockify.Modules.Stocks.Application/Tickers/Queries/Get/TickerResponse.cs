namespace Stockify.Modules.Stocks.Application.Tickers.Queries.Get;

public sealed record TickerResponse(
    string Symbol,
    string Name,
    string Description,
    string Cik,
    string Type);