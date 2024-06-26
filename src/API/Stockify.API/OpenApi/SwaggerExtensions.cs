namespace Stockify.API.OpenApi;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.ConfigureOptions<SwaggerGenOptionsSetup>();

        services.ConfigureOptions<SwaggerUIOptionsSetup>();

        services.AddSwaggerGen();

        return services;
    }
}
