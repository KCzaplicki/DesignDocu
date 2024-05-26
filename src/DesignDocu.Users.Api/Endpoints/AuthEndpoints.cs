using DesignDocu.Users.Application.Authorization.Login;
using DesignDocu.Users.Application.Authorization.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DesignDocu.Users.Api.Endpoints;

internal static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/login", async (LoginCommand command, ISender sender) =>
            await sender.Send(command))
            .WithName("GenerateToken");
        
        builder.MapPost("/refresh", async (RefreshTokenCommand command, ISender sender) =>
                await sender.Send(command))
            .WithName("RefreshToken");

        return builder;
    }
}