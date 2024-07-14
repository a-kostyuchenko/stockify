using FluentValidation;

namespace Stockify.Modules.Risks.Application.Questions.Commands.AddAnswer;

internal sealed class AddAnswerCommandValidator : AbstractValidator<AddAnswerCommand>
{
    public AddAnswerCommandValidator()
    {
        RuleFor(a => a.QuestionId)
            .NotEmpty();
        
        RuleFor(a => a.Content)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(a => a.Points)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}
