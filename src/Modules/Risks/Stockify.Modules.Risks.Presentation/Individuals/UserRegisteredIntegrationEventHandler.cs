using MediatR;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Individuals.Commands.Create;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Users.IntegrationEvents;

namespace Stockify.Modules.Risks.Presentation.Individuals;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreateIndividualCommand(
            IndividualId.From(integrationEvent.UserId),
            integrationEvent.FirstName,
            integrationEvent.LastName,
            integrationEvent.Email),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new StockifyException(nameof(CreateIndividualCommand), result.Error);
        }
    }
}
