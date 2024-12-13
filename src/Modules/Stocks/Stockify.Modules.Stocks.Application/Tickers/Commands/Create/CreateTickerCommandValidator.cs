using FluentValidation;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Application.Tickers.Commands.Create;

internal sealed class CreateTickerCommandValidator : AbstractValidator<CreateTickerCommand>
{
    public CreateTickerCommandValidator()
    {
        RuleFor(x => x.Symbol).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Cik).NotEmpty().MaximumLength(10);
        RuleFor(x => x.TickerTypeId).NotEmpty().NotEqual(TickerTypeId.Empty);
    }
}