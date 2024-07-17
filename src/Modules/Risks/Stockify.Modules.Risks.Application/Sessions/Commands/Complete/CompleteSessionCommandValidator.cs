using FluentValidation;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Complete;

internal sealed class CompleteSessionCommandValidator : AbstractValidator<CompleteSessionCommand>
{
    public CompleteSessionCommandValidator()
    {
        RuleFor(s => s.SessionId).NotEmpty();
        RuleFor(s => s.SessionId).NotEqual(SessionId.Empty);
    }
}
