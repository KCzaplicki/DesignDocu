using DesignDocu.Infrastructure.Auth.Authorization.Core.Jwt;
using DesignDocu.Infrastructure.Authorization.Abstractions.Authorization;
using DesignDocu.Infrastructure.Authorization.Abstractions.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace DesignDocu.Infrastructure.Auth.Authorization.Core.Authorization;

internal class AuthorizationService(UserManager<ApplicationUser> userManager, ITokenProvider tokenProvider) 
    : IAuthorizationService
{
    private const string RefreshTokenName = "RefreshToken";
    private const string LoginProviderName = nameof(DesignDocu);

    public async Task<bool> CheckPasswordAsync(string userId, string password)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<UserToken> GenerateTokenAsync(string userId)
    {
        var applicationUser = await userManager.FindByIdAsync(userId);

        return new UserToken(
            tokenProvider.GenerateAccessToken(applicationUser),
            tokenProvider.GenerateRefreshToken());
    }

    public async Task SetRefreshTokenAsync(string userId, string refreshToken)
    {
        var applicationUser = await userManager.FindByIdAsync(userId);
        
        await userManager.SetAuthenticationTokenAsync(applicationUser, LoginProviderName, RefreshTokenName, refreshToken);
    }

    public async Task<string> GetRefreshTokenAsync(string userId)
    {
        var applicationUser = await userManager.FindByIdAsync(userId);
        
        return await userManager.GetAuthenticationTokenAsync(applicationUser, LoginProviderName, RefreshTokenName);
    }
}