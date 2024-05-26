namespace DesignDocu.Infrastructure.Authorization.Abstractions.Authorization;

public record UserToken(string AccessToken, string RefreshToken);