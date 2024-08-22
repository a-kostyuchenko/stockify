using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Application.Stocks.Queries.GetMarketStatus;

namespace Stockify.Modules.Stocks.Presentation.Stocks;

internal sealed class GetMarketStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Stocks.GetMarketStatus, async (ISender sender) =>
        {
            var query = new GetMarketStatusQuery();

            Result<List<MarketResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Stocks)
        .WithName(nameof(GetMarketStatus))
        .Produces<List<MarketResponse>>();
    }
}
