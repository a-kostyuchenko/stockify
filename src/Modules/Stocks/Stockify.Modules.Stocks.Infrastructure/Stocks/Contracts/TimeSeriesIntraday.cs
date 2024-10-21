using System.Text.Json.Serialization;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

public sealed class TimeSeriesIntraday
{
    [JsonPropertyName("Meta Data")]
    public MetaData MetaData { get; set; }

    [JsonPropertyName("Time Series (5min)")]
    public Dictionary<string, TimeSeriesEntry> TimeSeries { get; set; }
}
