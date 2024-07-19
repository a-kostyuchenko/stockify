namespace Stockify.Modules.Risks.Presentation;

internal static class Routes
{
    internal static class Questions
    {
        private const string BaseUri = "questions";
        private const string ResourceId = "questionId";
        
        internal const string Get = $"{BaseUri}";
        internal const string GetById = $"{BaseUri}/{{{ResourceId}:guid}}";
        internal const string Create = $"{BaseUri}";
        internal const string AddAnswer = $"{BaseUri}/{{{ResourceId}:guid}}/answers";
    }
    
    internal static class Sessions
    {
        private const string BaseUri = "sessions";
        private const string ResourceId = "sessionId";
        
        internal const string Get = $"{BaseUri}";
        internal const string GetById = $"{BaseUri}/{{{ResourceId}:guid}}";
        internal const string GetQuestions = $"{BaseUri}/{{{ResourceId}:guid}}/questions";
        internal const string GetResult = $"{BaseUri}/{{{ResourceId}:guid}}/result";
        internal const string Create = $"{BaseUri}";
        internal const string Start = $"{BaseUri}/{{{ResourceId}:guid}}/start";
        internal const string SubmitAnswer = $"{BaseUri}/{{{ResourceId}:guid}}/submit-answer";
        internal const string Complete = $"{BaseUri}/{{{ResourceId}:guid}}/complete";
    }
}
