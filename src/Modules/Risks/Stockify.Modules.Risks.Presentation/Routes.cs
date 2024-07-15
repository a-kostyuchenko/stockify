namespace Stockify.Modules.Risks.Presentation;

internal static class Routes
{
    internal static class Questions
    {
        private const string BaseUri = "questions";
        private const string ResourceId = "questionId";
        
        internal const string GetQuestions = $"{BaseUri}";
        internal const string GetQuestion = $"{BaseUri}/{{{ResourceId}:guid}}";
        internal const string Create = $"{BaseUri}";
        internal const string AddAnswer = $"{BaseUri}/{{{ResourceId}:guid}}/answers";
    }
}
