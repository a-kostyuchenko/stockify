using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Stockify.API.OpenApi;

internal sealed class SwaggerUIOptionsSetup(IApiVersionDescriptionProvider descriptionProvider) 
    : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        options.DisplayRequestDuration();

        foreach (ApiVersionDescription description in descriptionProvider.ApiVersionDescriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }
        
        options.EnableTryItOutByDefault();
        options.EnablePersistAuthorization();
    }
}
