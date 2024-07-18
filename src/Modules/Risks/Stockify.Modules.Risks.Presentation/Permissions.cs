namespace Stockify.Modules.Risks.Presentation;

internal static class Permissions
{
    internal const string GetQuestions = "questions:read";
    internal const string ModifyQuestions = "questions:modify";
    
    internal const string GetSessions = "sessions:read";
    internal const string GetSessionQuestions = "sessions:access_questions";
    internal const string ModifySessions = "sessions:modify";
}
