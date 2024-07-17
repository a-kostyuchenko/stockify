using FluentValidation;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Application.Questions.Commands.AddAnswer;

internal sealed class AddAnswerCommandValidator : AbstractValidator<AddAnswerCommand>
{
    public AddAnswerCommandValidator()
    {
        RuleFor(a => a.QuestionId)
            .NotEmpty();
        
        RuleFor(a => a.QuestionId)
            .NotEqual(QuestionId.Empty);
        
        RuleFor(a => a.Content)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(a => a.Points)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}
