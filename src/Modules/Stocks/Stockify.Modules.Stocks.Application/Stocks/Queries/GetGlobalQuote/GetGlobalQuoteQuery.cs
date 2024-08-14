using Stockify.Common.Application.Messaging;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;

namespace Stockify.Modules.Stocks.Application.Stocks.Queries.GetGlobalQuote;

public sealed record GetGlobalQuoteQuery(string Symbol) : IQuery<QuoteResponse>;
