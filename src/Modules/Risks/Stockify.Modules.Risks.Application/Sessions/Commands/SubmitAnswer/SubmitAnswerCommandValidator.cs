using FluentValidation;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.SubmitAnswer;

internal sealed class SubmitAnswerCommandValidator : AbstractValidator<SubmitAnswerCommand>
{
    public SubmitAnswerCommandValidator()
    {
        RuleFor(s => s.SessionId).NotEmpty();
        RuleFor(s => s.SessionId).NotEqual(SessionId.Empty);
        
        RuleFor(s => s.QuestionId).NotEmpty();
        RuleFor(s => s.QuestionId).NotEqual(QuestionId.Empty);
        
        RuleFor(s => s.AnswerId).NotEmpty();
        RuleFor(s => s.AnswerId).NotEqual(AnswerId.Empty);
    }
}
