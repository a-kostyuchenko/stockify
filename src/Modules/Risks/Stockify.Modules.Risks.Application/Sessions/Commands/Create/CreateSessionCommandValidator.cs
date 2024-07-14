using FluentValidation;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Create;

internal sealed class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
{
    public CreateSessionCommandValidator()
    {
        RuleFor(x => x.IndividualId).NotEmpty();
        RuleFor(x => x.QuestionsCount).InclusiveBetween(Session.MinQuestionsCount, Session.MaxQuestionsCount);
    }
}
