using System.Text.Json.Serialization;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

public sealed class MarketStatusData
{
    [JsonPropertyName("endpoint")]
    public string Endpoint { get; set; }

    [JsonPropertyName("markets")]
    public List<MarketData> Markets { get; set; }
}
