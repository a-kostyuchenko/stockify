using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Stockify.Common.Infrastructure.EventBus;

internal sealed class QueueOptionsSetup(IConfiguration configuration) : IConfigureNamedOptions<QueueOptions>
{
    private const string ConfigurationSection = "Queue";
    
    public void Configure(QueueOptions options) => 
        configuration.GetSection(ConfigurationSection).Bind(options);

    public void Configure(string? name, QueueOptions options) => 
        Configure(options);
}
