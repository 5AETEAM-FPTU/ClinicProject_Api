using System;
using System.Threading.Tasks;
using Clinic.WebAPI.Commons.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI.Commons.Middleware;

/// <summary>
///     Global Exception Handler
/// </summary>
internal sealed class GlobalExceptionHandler
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            if (!httpContext.Response.HasStarted)
            {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await httpContext.Response.WriteAsJsonAsync(
                    value: new CommonApiResponse
                    {
                        AppCode = StatusCodes.Status500InternalServerError.ToString(),
                        ErrorMessages = ["Server has encountered an error !", exception.Message]
                    }
                );

                await httpContext.Response.CompleteAsync();
            }
        }
    }
}
