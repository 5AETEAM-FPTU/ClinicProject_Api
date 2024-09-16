using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Clinic.WebAPI.Commons.Behaviors.Logging;

/// <summary>
///     Preprocessor for request logging.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public class RequestLoggingPreProcessor<TRequest> : IPreProcessor<TRequest>
{
    private readonly ILogger<RequestLoggingPreProcessor<TRequest>> _logger;

    public RequestLoggingPreProcessor(ILogger<RequestLoggingPreProcessor<TRequest>> logger)
    {
        _logger = logger;
    }

    public Task PreProcessAsync(IPreProcessorContext<TRequest> context, CancellationToken ct)
    {
        _logger.LogInformation(
            $"request:{context.Request.GetType().FullName} path: {context.HttpContext.Request.Path}"
        );

        return Task.CompletedTask;
    }
}
