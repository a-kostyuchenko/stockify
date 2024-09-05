using Stockify.Common.Application.Messaging;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Application.Stockholders.Commands.Create;

public sealed record CreateStockholderCommand(
    StockholderId Id,
    string FirstName,
    string LastName,
    string Email) : ICommand;
