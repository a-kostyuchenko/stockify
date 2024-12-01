using System.Text.Json.Serialization;

namespace Stockify.Common.Presentation.Filters.Idempotency;

[method: JsonConstructor]
internal sealed class IdempotentResponse(int statusCode, object? value)
{
    public int StatusCode { get; } = statusCode;
    public object? Value { get; } = value;
}
