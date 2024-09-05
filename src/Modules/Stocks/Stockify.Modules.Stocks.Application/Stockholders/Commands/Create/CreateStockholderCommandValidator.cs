using FluentValidation;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Application.Stockholders.Commands.Create;

internal sealed class CreateStockholderCommandValidator : AbstractValidator<CreateStockholderCommand>
{
    public CreateStockholderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).NotEqual(StockholderId.Empty);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}