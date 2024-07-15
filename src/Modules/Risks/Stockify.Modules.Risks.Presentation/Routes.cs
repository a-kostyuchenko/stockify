namespace Stockify.Modules.Risks.Presentation;

internal static class Routes
{
    internal static class Questions
    {
        internal const string BaseUri = "questions";
        internal const string ResourceId = "questionId";
        
        internal const string Create = $"{BaseUri}";
        internal const string AddAnswer = $"{BaseUri}/{{{ResourceId}:guid}}";
    }
}
