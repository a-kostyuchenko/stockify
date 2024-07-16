using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public static class AnswerErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Answer.NotFound",
        "Answer with specified identifier was not found");
    
    public static readonly Error MismatchedQuestion = Error.Problem(
        "Answer.MismatchedQuestion",
        "Answer does not belong to question");
}
