using FluentValidation;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Create;

internal sealed class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
{
    public CreateSessionCommandValidator()
    {
        RuleFor(x => x.IndividualId).NotEmpty();
        RuleFor(x => x.IndividualId).NotEqual(IndividualId.Empty);
    }
}
