using FluentValidation;

namespace Stockify.Modules.Risks.Application.Individuals.Commands.Create;

internal sealed class CreateIndividualCommandValidator : AbstractValidator<CreateIndividualCommand>
{
    public CreateIndividualCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
