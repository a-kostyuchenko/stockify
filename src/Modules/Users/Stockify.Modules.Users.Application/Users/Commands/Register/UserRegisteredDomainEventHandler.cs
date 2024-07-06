using MediatR;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Users.Queries.GetById;
using Stockify.Modules.Users.Domain.Users.Events;
using Stockify.Modules.Users.IntegrationEvents;

namespace Stockify.Modules.Users.Application.Users.Commands.Register;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus eventBus) 
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
        
        await eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
