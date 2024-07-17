using FluentValidation;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Individuals.Commands.Create;

internal sealed class CreateIndividualCommandValidator : AbstractValidator<CreateIndividualCommand>
{
    public CreateIndividualCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).NotEqual(IndividualId.Empty);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
