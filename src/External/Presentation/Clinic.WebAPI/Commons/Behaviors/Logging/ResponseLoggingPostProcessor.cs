using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Clinic.WebAPI.Commons.Behaviors.Logging;

/// <summary>
///     Postprocessor for response logging.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ResponseLoggingPostProcessor<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    private readonly ILogger<ResponseLoggingPostProcessor<TRequest, TResponse>> _logger;

    public ResponseLoggingPostProcessor(
        ILogger<ResponseLoggingPostProcessor<TRequest, TResponse>> logger
    )
    {
        _logger = logger;
    }

    public Task PostProcessAsync(
        IPostProcessorContext<TRequest, TResponse> context,
        CancellationToken ct
    )
    {
        _logger.LogInformation(
            $"request:{context.Response.GetType().FullName} path: {context.HttpContext.Response.Path}"
        );

        return Task.CompletedTask;
    }
}
