using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Stockify.API.OpenApi;

internal sealed class SwaggerUIOptionsSetup : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options) => 
        options.DisplayRequestDuration();
}
