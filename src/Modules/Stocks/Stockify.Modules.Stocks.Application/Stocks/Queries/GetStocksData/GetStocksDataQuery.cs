using Stockify.Common.Application.Messaging;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;

namespace Stockify.Modules.Stocks.Application.Stocks.Queries.GetStocksData;

public sealed record GetStocksDataQuery(string Ticker) : IQuery<List<TimeSeriesResponse>>;
