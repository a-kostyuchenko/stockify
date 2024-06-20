using MediatR;
using Stockify.Common.Domain;

namespace Stockify.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
