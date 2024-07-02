namespace Stockify.Modules.Users.Application.Abstractions.Identity;

public record TokenResponse(string AccessToken, string RefreshToken);
