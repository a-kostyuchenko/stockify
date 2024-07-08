using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Stockify.Common.Infrastructure.EventBus;

internal sealed class QueueOptionsSetup(IConfiguration configuration) : IConfigureNamedOptions<QueueOptions>
{
    public void Configure(QueueOptions options) => 
        configuration.GetSection(QueueOptions.ConfigurationSection).Bind(options);

    public void Configure(string? name, QueueOptions options) => 
        Configure(options);
}
