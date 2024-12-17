using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Application.Pagination;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Stocks.Application.Tickers.Queries.Get;

namespace Stockify.Modules.Stocks.Presentation.Tickers;

internal sealed class GetTickers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Tickers.Get, async (
                ISender sender,
                string? searchTerm,
                int page = 1,
                int pageSize = 15) =>
            {
                var command = new GetTickersQuery(searchTerm, page, pageSize);

                Result<PagedResponse<TickerResponse>> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Tickers)
            .Produces<PagedResponse<TickerResponse>>();
    }
}
