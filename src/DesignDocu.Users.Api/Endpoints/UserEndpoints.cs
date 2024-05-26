using DesignDocu.Users.Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DesignDocu.Users.Api.Endpoints;

internal static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/register", async (RegisterCommand command, ISender sender) 
                => await sender.Send(command))
            .WithName("Register");
        
        return builder;
    }
}