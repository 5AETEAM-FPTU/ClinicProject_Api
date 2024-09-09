using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.HelloWorld;

/// <summary>
///     HelloWorld request handler.
/// </summary>
internal sealed class HelloWorldHandler : IFeatureHandler<HelloWorldRequest, HelloWorldResponse>
{
    public HelloWorldHandler() { }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="command">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<HelloWorldResponse> ExecuteAsync(
        HelloWorldRequest request,
        CancellationToken ct
    )
    {
        // Repository can be call here...

        // After some actions ...

        // Create strings
        var description = $"Hello {request.FirstName} {request.LastName}";

        return new()
        {
            StatusCode = HelloWorldResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { Description = description }
        };
    }
}
