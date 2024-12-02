using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Stockify.Common.Application.Caching;

namespace Stockify.Common.Presentation.Filters.Idempotency;

public sealed class IdempotencyFilter(int cacheTimeInMinutes = 60) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (!TryGetIdempotencyKey(out Guid idempotencyKey))
        {
            return Microsoft.AspNetCore.Http.Results.BadRequest("Missing Idempotency-Key header");
        }
        
        ICacheService cache = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

        string cacheKey = $"Idempotent_{idempotencyKey}";
        IdempotentResponse? cachedResult = await cache.GetAsync<IdempotentResponse>(cacheKey);

        if (cachedResult is not null)
        {
            return new IdempotentResult(cachedResult.StatusCode, cachedResult.Value);
        }

        object? result = await next(context);

        if (result is IStatusCodeHttpResult { StatusCode: >= 200 and < 300 } statusCodeResult and
            IValueHttpResult valueResult)
        {
            int statusCode = statusCodeResult.StatusCode ?? StatusCodes.Status200OK;
            IdempotentResponse response = new(statusCode, valueResult.Value);

            await cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(cacheTimeInMinutes));
        }

        return result;

        bool TryGetIdempotencyKey(out Guid key)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Idempotency-Key", out StringValues keyValue))
            {
                return Guid.TryParse(keyValue, out key);
            }

            key = Guid.Empty;
            return false;
        }
    }
}
