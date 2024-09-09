using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.HelloWorld;
using Clinic.WebAPI.EndPoints.HelloWorld.Common;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI.EndPoints.HelloWorld.Middleware.Caching;

/// <summary>
///     HelloWorld caching pre processor.
/// </summary>
internal sealed class HelloWorldCachingPreProcessor
    : PreProcessor<HelloWorldRequest, HelloWorldStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HelloWorldCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<HelloWorldRequest> context,
        HelloWorldStateBag state,
        CancellationToken ct
    ) { }
}
