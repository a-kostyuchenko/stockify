using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public static class QuestionErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Question.NotFound",
        "Question with specified identifier was not found");
    
    public static readonly Error NotEnoughAnswers = Error.Problem(
        "Question.NotEnoughAnswers",
        "Not enough answers provided");

    public static readonly Error TooManyAnswers = Error.Problem(
        "Question.TooManyAnswers",
        "Too many answers provided");
}
