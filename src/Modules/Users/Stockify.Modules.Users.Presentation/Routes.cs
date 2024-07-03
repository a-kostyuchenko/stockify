namespace Stockify.Modules.Users.Presentation;

internal static class Routes
{
    internal static class Users
    {
        internal const string BaseUri = "users";
        internal const string ResourceId = "userId";
        internal const string GetUser = $"{BaseUri}/{{{ResourceId}:guid}}";
        internal const string GetProfile = $"{BaseUri}/profile";
        internal const string Register = $"{BaseUri}/register";
    }
    
    internal static class Authentication
    {
        internal const string BaseUri = "authentication";
        internal const string Login = $"{BaseUri}/login";
        internal const string RefreshToken = $"{BaseUri}/refresh-token";
    }
}
