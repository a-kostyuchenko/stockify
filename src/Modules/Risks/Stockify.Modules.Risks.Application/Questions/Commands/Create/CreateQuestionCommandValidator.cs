using FluentValidation;

namespace Stockify.Modules.Risks.Application.Questions.Commands.Create;

internal sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(500);
    }
}
