using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.HelloWorld;
using Clinic.WebAPI.EndPoints.HelloWorld.Common;
using Clinic.WebAPI.EndPoints.HelloWorld.HttpResponseMapper;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI.EndPoints.HelloWorld.Middleware.Caching;

/// <summary>
///     HelloWorld caching post processor.
/// </summary>
internal sealed class HelloWorldCachingPostProcessor
    : PostProcessor<HelloWorldRequest, HelloWorldStateBag, HelloWorldHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HelloWorldCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<HelloWorldRequest, HelloWorldHttpResponse> context,
        HelloWorldStateBag state,
        CancellationToken ct
    ) { }
}
