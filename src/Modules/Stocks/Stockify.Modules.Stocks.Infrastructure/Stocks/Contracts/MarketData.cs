using System.Text.Json.Serialization;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

public sealed class MarketData
{
    [JsonPropertyName("market_type")]
    public string MarketType { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("primary_exchanges")]
    public string PrimaryExchanges { get; set; }

    [JsonPropertyName("local_open")]
    public string LocalOpen { get; set; }

    [JsonPropertyName("local_close")]
    public string LocalClose { get; set; }

    [JsonPropertyName("current_status")]
    public string CurrentStatus { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }
}
