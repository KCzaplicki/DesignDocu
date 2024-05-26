namespace DesignDocu.Infrastructure.Authorization.Abstractions.Authorization;

public interface IAuthorizationService
{
    Task<bool> CheckPasswordAsync(string userId, string password);
    Task<UserToken> GenerateTokenAsync(string userId);
    Task SetRefreshTokenAsync(string userId, string refreshToken);
    Task<string> GetRefreshTokenAsync(string userId);
}