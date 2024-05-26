using Microsoft.AspNetCore.Builder;

namespace DesignDocu.Common.Api.ErrorHandling;

public static class ServiceExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ErrorHandlingMiddleware>();
        
        return builder;
    }
}