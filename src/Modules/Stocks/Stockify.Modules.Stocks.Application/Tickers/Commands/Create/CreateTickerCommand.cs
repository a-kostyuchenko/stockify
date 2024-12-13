using Stockify.Common.Application.Messaging;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Application.Tickers.Commands.Create;

public sealed record CreateTickerCommand(
    string Symbol,
    string Name,
    string Description,
    string Cik,
    TickerTypeId TickerTypeId) : ICommand<Guid>;
