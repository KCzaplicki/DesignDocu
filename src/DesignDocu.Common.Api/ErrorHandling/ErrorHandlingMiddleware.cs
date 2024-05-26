using System.Net.Mime;
using DesignDocu.Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DesignDocu.Common.Api.ErrorHandling;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }
    
    private async Task HandleException(HttpContext context, Exception ex)
    {
        switch (ex)
        {
            case NotFoundException:
                await SetErrorResponseAsync(context, StatusCodes.Status404NotFound, ex.Message);
                break;
            case UnauthorizedException:
                await SetErrorResponseAsync(context, StatusCodes.Status401Unauthorized, ex.Message);
                break;
            default:
                throw ex;
        }
    }
    
    private async Task SetErrorResponseAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = MediaTypeNames.Text.Plain;
        await context.Response.WriteAsync(message);
    }
}