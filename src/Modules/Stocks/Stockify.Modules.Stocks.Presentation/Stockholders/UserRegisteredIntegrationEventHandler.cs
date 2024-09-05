using MediatR;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Stockholders.Commands.Create;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Users.IntegrationEvents;

namespace Stockify.Modules.Stocks.Presentation.Stockholders;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateStockholderCommand(
            StockholderId.From(integrationEvent.UserId),
            integrationEvent.FirstName,
            integrationEvent.LastName,
            integrationEvent.LastName);

        Result result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new StockifyException(nameof(CreateStockholderCommand), result.Error);
        }
    }
}
