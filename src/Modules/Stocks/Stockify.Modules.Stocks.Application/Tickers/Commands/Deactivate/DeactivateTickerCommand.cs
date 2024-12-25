using Stockify.Common.Application.Messaging;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Application.Tickers.Commands.Deactivate;

public sealed record DeactivateTickerCommand(TickerId TickerId) : ICommand;
