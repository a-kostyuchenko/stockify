using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;
using Stockify.Modules.Stocks.Application.Stocks.Queries.GetStocksData;

namespace Stockify.Modules.Stocks.Presentation.Stocks;

internal sealed class GetStocksData : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Stocks.GetStocksData, async (string ticker, ISender sender) =>
            {
                var query = new GetStocksDataQuery(ticker);

                Result<List<TimeSeriesResponse>> result = await sender.Send(query);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Stocks)
            .WithName(nameof(GetStocksData))
            .Produces<List<TimeSeriesResponse>>();
    }
}
