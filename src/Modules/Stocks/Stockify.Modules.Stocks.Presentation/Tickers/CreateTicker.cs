using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Stocks.Application.Tickers.Commands.Create;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Presentation.Tickers;

internal sealed class CreateTicker : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Tickers.Create, async (Request request, ISender sender) =>
        {
            var command = new CreateTickerCommand(
                request.Symbol,
                request.Name,
                request.Description,
                request.Cik,
                TickerTypeId.From(request.TickerTypeId));

            Result<string> result = await sender.Send(command);

            return result.Match(Results.Created, ApiResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Tickers);
    }

    internal sealed record Request
    {
        public string Symbol { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Cik { get; init; }
        public Guid TickerTypeId { get; init; }
    }
}
