using Stockify.Common.Application.Messaging;

namespace Stockify.Modules.Stocks.Application.TickerTypes.Commands.Create;

public sealed record CreateTickerTypeCommand(string Code, string Description) : ICommand<Guid>;
