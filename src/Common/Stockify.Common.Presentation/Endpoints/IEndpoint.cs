using Microsoft.AspNetCore.Routing;

namespace Stockify.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
