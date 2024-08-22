using Stockify.Common.Application.Messaging;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;

namespace Stockify.Modules.Stocks.Application.Stocks.Queries.GetMarketStatus;

public sealed record GetMarketStatusQuery() : IQuery<List<MarketResponse>>;