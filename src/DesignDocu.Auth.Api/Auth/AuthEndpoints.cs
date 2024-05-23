using DesignDocu.Auth.Api.Auth.Models;
using DesignDocu.Authorization.DataAccess.Models;
using DesignDocu.Authorization.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace DesignDocu.Auth.Api.Auth;

internal static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/token", async (
                TokenRequestModel model,
                UserManager<ApplicationUser> userManager,
                ITokenProvider tokenProvider) =>
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var accessToken = tokenProvider.GenerateAccessToken(user);
                    var refreshToken = tokenProvider.GenerateRefreshToken();

                    await userManager.SetAuthenticationTokenAsync(user, nameof(DesignDocu), "RefreshToken", refreshToken);

                    return Results.Ok(new TokenResponseModel
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    });
                }

                return Results.Unauthorized();
            })
            .WithName("GenerateToken");

        builder.MapPost("/refresh", async (
                RefreshTokenRequestModel model, 
                UserManager<ApplicationUser> userManager, 
                ITokenProvider tokenProvider) =>
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return Results.Unauthorized();
                }

                var storedRefreshToken = await userManager.GetAuthenticationTokenAsync(user, nameof(DesignDocu), "RefreshToken");
                if (storedRefreshToken != model.RefreshToken)
                {
                    return Results.Unauthorized();
                }

                var newAccessToken = tokenProvider.GenerateAccessToken(user);
                var newRefreshToken = tokenProvider.GenerateRefreshToken();

                await userManager.SetAuthenticationTokenAsync(user, nameof(DesignDocu), "RefreshToken", newRefreshToken);

                return Results.Ok(new TokenResponseModel
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            })
            .WithName("RefreshToken");

        return builder;
    }
}