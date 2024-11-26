using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;

namespace Stockify.Modules.Stocks.Application.Stocks.Queries.GetGlobalQuote;

internal sealed class GetGlobalQuoteQueryHandler(IStocksService stocksService) 
    : IQueryHandler<GetGlobalQuoteQuery, QuoteResponse>
{
    public async Task<Result<QuoteResponse>> Handle(
        GetGlobalQuoteQuery request,
        CancellationToken cancellationToken) =>
        await stocksService.GetQuoteAsync(request.Ticker, cancellationToken);
}
