using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Stockify.Common.Application.Behaviors;

[SuppressMessage("Major Code Smell", "S2139:Exceptions should be either logged or rethrown but not both")]
internal sealed class ExceptionHandlingBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception for {RequestName}", typeof(TRequest).Name);

            throw;
        }
    }
}
