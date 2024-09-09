using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.HelloWorld;
using Clinic.WebAPI.EndPoints.HelloWorld.Common;
using FastEndpoints;

namespace Clinic.WebAPI.EndPoints.HelloWorld.Middleware.Authorization;

/// <summary>
///     HelloWorld authorizatio pre processor.
/// </summary>
internal sealed class HelloWorldAuthorizationPreProcessor
    : PreProcessor<HelloWorldRequest, HelloWorldStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<HelloWorldRequest> context,
        HelloWorldStateBag state,
        CancellationToken ct
    ) { }
}
