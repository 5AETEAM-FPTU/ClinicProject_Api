using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.HelloWorld;
using Clinic.WebAPI.EndPoints.HelloWorld.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.HelloWorld;

/// <summary>
///     HelloWorld endpoint.
/// </summary>
internal sealed class HelloWorldEndpoint : Endpoint<HelloWorldRequest, HelloWorldHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "hello-world");
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for HelloWorld feature";
            summary.Description = "This endpoint is used for HelloWorld purpose.";
            summary.ExampleRequest = new() { FirstName = "string", LastName = "string", };
            summary.Response<HelloWorldHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = HelloWorldResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new HelloWorldResponse.Body() { Description = "string" }
                }
            );
        });
    }

    public override async Task<HelloWorldHttpResponse> ExecuteAsync(
        HelloWorldRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = HelloWorldHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        /*
        * Store the real http code of http response into a temporary variable.
        * Set the http code of http response to default for not serializing.
        */
        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        // Send http response to client.
        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct
        );

        // Set the http code of http response back to real one.
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
