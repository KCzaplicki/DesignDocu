using DesignDocu.Auth.Api.Auth;
using Microsoft.AspNetCore.Routing;

namespace DesignDocu.Auth.Api;

public static class ServiceExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        AuthEndpoints.MapAuthEndpoints(builder);
        
        return builder;
    }
}