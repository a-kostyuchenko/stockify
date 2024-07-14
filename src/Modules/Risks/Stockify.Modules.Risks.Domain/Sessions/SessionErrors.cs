using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Sessions;

public static class SessionErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Session.NotFound",
        "Session with specified identifier was not found");

    public static readonly Error MaxQuestionsExceeded = Error.Problem(
        "Session.MaxQuestionsExceeded",
        "Maximum number of questions exceeded");

    public static readonly Error InvalidQuestionsCount = Error.Problem(
        "Session.InvalidQuestionsCount",
        "Invalid number of questions provided");

    public static readonly Error NotEnoughQuestions = Error.Problem(
        "Session.NotEnoughQuestions",
        "Not enough questions provided");
}
