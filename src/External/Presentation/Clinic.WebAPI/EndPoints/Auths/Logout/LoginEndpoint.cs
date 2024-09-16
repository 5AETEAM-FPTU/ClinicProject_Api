using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.Logout;
using Clinic.WebAPI.EndPoints.Auths.Logout.Common;
using Clinic.WebAPI.EndPoints.Auths.Logout.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.Logout;

/// <summary>
///     Logout endpoint.
/// </summary>
internal sealed class LogoutEndpoint : Endpoint<EmptyRequest, LogoutHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/logout");
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Logout feature";
            summary.Description = "This endpoint is used for Logout purpose.";
            summary.ExampleRequest = new() { };
            summary.Response<LogoutHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LogoutResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<LogoutHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var stateBag = ProcessorState<LogoutStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LogoutHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: stateBag.AppRequest, arg2: appResponse);

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
