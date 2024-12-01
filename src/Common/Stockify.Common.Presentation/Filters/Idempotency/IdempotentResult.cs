using Microsoft.AspNetCore.Http;

namespace Stockify.Common.Presentation.Filters.Idempotency;

internal sealed class IdempotentResult(int statusCode, object? value) : IResult
{
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = statusCode;

        return httpContext.Response.WriteAsJsonAsync(value);
    }
}
