namespace Stockify.Modules.Stocks.Application.Tickers.Queries.Get;

public sealed record GetTickersParameters(
    string? SearchTerm,
    int Take,
    int Skip);