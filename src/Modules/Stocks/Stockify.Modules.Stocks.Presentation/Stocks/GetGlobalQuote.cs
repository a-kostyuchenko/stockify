using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Application.Stocks.Queries.GetGlobalQuote;

namespace Stockify.Modules.Stocks.Presentation.Stocks;

internal sealed class GetGlobalQuote : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Stocks.GetGlobalQuote, async (string symbol, ISender sender) =>
        {
            var query = new GetGlobalQuoteQuery(symbol);

            Result<QuoteResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Stocks)
        .WithName(nameof(GetGlobalQuote))
        .Produces<QuoteResponse>();
    }
}
