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

    public static readonly Error InvalidStatus = Error.Problem(
        "Session.InvalidStatus",
        "Session status is invalid");
    
    public static readonly Error AlreadyStarted = Error.Problem(
        "Session.AlreadyStarted",
        "Session has already been started");

    public static readonly Error MismatchedQuestion = Error.Problem(
        "Session.MismatchedQuestion",
        "Question does not belong to session");
    
    public static readonly Error IncompleteSubmissions = Error.Problem(
        "Session.IncompleteSubmissions",
        "Not all questions in the session have a submitted answer");

    public static readonly Error IndividualMismatch = Error.Problem(
        "Session.IndividualMismatch",
        "Session does not belong to the individual");
}
