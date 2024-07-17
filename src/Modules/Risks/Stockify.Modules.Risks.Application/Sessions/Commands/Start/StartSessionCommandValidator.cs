using FluentValidation;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Start;

internal sealed class StartSessionCommandValidator : AbstractValidator<StartSessionCommand>
{
    public StartSessionCommandValidator()
    {
        RuleFor(s => s.SessionId).NotEmpty();
        RuleFor(s => s.SessionId).NotEqual(SessionId.Empty);
    }
}
