using Stockify.Common.Domain;

namespace Stockify.Common.Application.Exceptions;

public class StockifyException(
    string requestName,
    Error? error = default,
    Exception? innerException = default) 
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;
    public Error? Error { get; } = error;
}
