using FluentValidation;

namespace Stockify.Modules.Stocks.Application.TickerTypes.Commands.Create;

internal sealed class CreateTickerTypeCommandValidator : AbstractValidator<CreateTickerTypeCommand>
{
    public CreateTickerTypeCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
    }
}