using MediatR;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Users.Queries.GetById;
using Stockify.Modules.Users.Domain.Users.Events;
using Stockify.Modules.Users.IntegrationEvents;

namespace Stockify.Modules.Users.Application.Users.Commands.Update;

internal sealed class UserUpdatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<UserUpdatedDomainEvent>
{
    public override async Task Handle(
        UserUpdatedDomainEvent domainEvent,
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
            new UserUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
