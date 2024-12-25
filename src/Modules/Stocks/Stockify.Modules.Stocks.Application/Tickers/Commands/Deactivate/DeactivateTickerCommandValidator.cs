using FluentValidation;

namespace Stockify.Modules.Stocks.Application.Tickers.Commands.Deactivate;

internal sealed class DeactivateTickerCommandValidator : AbstractValidator<DeactivateTickerCommand>
{
    public DeactivateTickerCommandValidator()
    {
        RuleFor(c => c.TickerId).NotEmpty();
    }
}