using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Stocks.Application.TickerTypes.Commands.Create;

namespace Stockify.Modules.Stocks.Presentation.TickerTypes;

internal sealed class CreateTickerType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.TickerTypes.Create, async (Request request, ISender sender) =>
        {
            var command = new CreateTickerTypeCommand(request.Code, request.Description);

            Result<Guid> result = await sender.Send(command);
            
            return result.Match(Results.Created, ApiResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.TickerTypes);
    }
    
    internal sealed record Request
    {
        public string Code { get; init; }

        public string Description { get; init; }
    }
}
