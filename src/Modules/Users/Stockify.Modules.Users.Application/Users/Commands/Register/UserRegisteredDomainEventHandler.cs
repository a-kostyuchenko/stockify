using MediatR;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Users.Queries.GetById;
using Stockify.Modules.Users.Domain.Users.Events;

namespace Stockify.Modules.Users.Application.Users.Commands.Register;

internal sealed class UserRegisteredDomainEventHandler(ISender sender) 
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(
        UserRegisteredDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserByIdQuery(domainEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new StockifyException(nameof(GetUserByIdQuery), result.Error);
        }
        
        // Publish integration event here
    }
}
