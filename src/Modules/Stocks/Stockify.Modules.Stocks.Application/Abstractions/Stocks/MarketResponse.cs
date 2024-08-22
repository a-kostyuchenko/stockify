namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public sealed record MarketResponse(
    string MarketType,
    string Region,
    string PrimaryExchanges,
    string LocalOpen,
    string LocalClose,
    string CurrentStatus,
    string Notes);
