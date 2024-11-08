namespace Stockify.Modules.Users.Presentation;

internal static class Routes
{
    internal static class Users
    {
        private const string BaseUri = "users";
        private const string ResourceId = "userId";

        internal const string GetUser = $"{BaseUri}/{{{ResourceId}:guid}}";
        internal const string GetProfile = $"{BaseUri}/profile";
        internal const string Register = $"{BaseUri}/register";
        internal const string UpdateProfile = $"{BaseUri}/profile";
    }

    internal static class Authentication
    {
        private const string BaseUri = "authentication";

        internal const string Login = $"{BaseUri}/login";
        internal const string RefreshToken = $"{BaseUri}/refresh-token";
    }
}
