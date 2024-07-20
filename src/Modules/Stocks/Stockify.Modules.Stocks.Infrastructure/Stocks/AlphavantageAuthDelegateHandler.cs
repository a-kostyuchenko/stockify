using Microsoft.Extensions.Options;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks;

internal sealed class AlphavantageAuthDelegateHandler(IOptions<AlphavantageOptions> options) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder(request.RequestUri!);

        uriBuilder.Query = string.IsNullOrEmpty(uriBuilder.Query) ? 
            $"apikey={options.Value.ApiKey}" :
            $"{uriBuilder.Query}&apikey={options.Value.ApiKey}";
        
        request.RequestUri = uriBuilder.Uri;
        
        return base.SendAsync(request, cancellationToken);
    }
}
