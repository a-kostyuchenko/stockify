using System.Text.Json.Serialization;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

public sealed class GlobalQuoteData
{
    [JsonPropertyName("01. symbol")]
    public string Symbol { get; init; }

    [JsonPropertyName("02. open")]
    public string Open { get; init; }

    [JsonPropertyName("03. high")]
    public string High { get; init; }

    [JsonPropertyName("04. low")]
    public string Low { get; init; }

    [JsonPropertyName("05. price")]
    public string Price { get; init; }

    [JsonPropertyName("06. volume")]
    public string Volume { get; init; }

    [JsonPropertyName("07. latest trading day")]
    public string LatestTradingDay { get; init; }

    [JsonPropertyName("08. previous close")]
    public string PreviousClose { get; init; }

    [JsonPropertyName("09. change")]
    public string Change { get; init; }

    [JsonPropertyName("10. change percent")]
    public string ChangePercent { get; init; }
}
