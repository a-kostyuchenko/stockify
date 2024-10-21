using System.Text.Json.Serialization;

namespace Stockify.Modules.Stocks.Infrastructure.Stocks.Contracts;

public sealed class GlobalQuote
{
    [JsonPropertyName("Global Quote")]
    public GlobalQuoteEntry? Data { get; init; }
}
