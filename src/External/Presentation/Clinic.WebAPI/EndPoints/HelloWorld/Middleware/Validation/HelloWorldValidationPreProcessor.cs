using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.HelloWorld;
using Clinic.WebAPI.EndPoints.HelloWorld.Common;
using FastEndpoints;

namespace Clinic.WebAPI.EndPoints.HelloWorld.Middleware.Validation;

/// <summary>
///     PreProcessor for HelloWorld for validation
/// </summary>

internal sealed class HelloWorldValidationPreProcessor
    : PreProcessor<HelloWorldRequest, HelloWorldStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<HelloWorldRequest> context,
        HelloWorldStateBag state,
        CancellationToken ct
    ) { }
}
