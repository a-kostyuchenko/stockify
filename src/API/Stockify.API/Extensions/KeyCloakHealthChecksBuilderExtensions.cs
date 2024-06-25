using Stockify.Common.Infrastructure.Configuration;

namespace Stockify.API.Extensions;

internal static class KeyCloakHealthChecksBuilderExtensions
{
    private const string KeyCloakHealthCheck = "KeyCloak";
    private const string KeyCloakHealthUrl = "KeyCloak:HealthUrl";

    internal static IHealthChecksBuilder AddKeyCloak(this IHealthChecksBuilder builder, Uri healthUri)
    {
        builder.AddUrlGroup(healthUri, HttpMethod.Get, KeyCloakHealthCheck);

        return builder;
    }

    internal static Uri GetKeyCloakHealthUrl(this IConfiguration configuration) => 
        new(configuration.GetValueOrThrow<string>(KeyCloakHealthUrl)!);
}
