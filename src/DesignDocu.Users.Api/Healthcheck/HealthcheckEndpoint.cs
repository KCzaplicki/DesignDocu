using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DesignDocu.Users.Api.Healthcheck;

internal static class HealthcheckEndpoint
{
    public static RouteGroupBuilder MapHealthcheck(this RouteGroupBuilder builder)
    {
        builder.Map("/healthcheck", () => "Healthy");
        
        return builder;
    }
}