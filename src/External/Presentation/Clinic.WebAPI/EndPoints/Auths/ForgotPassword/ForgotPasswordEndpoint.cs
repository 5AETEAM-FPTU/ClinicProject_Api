using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.ForgotPassword;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.ForgotPassword.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ForgotPassword;

/// <summary>
///     ForgotPassword endpoint.
/// </summary>
internal sealed class ForgotPasswordEndpoint
    : Endpoint<ForgotPasswordRequest, ForgotPasswordHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/forgot-password");
        PreProcessor<ValidationPreProcessor<ForgotPasswordRequest>>();
        AllowAnonymous();
        DontThrowIfValidationFails();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for sending reset password OTP code.";
            summary.Description = "This endpoint is used for forgot password purpose.";
            summary.ExampleRequest = new() { Email = "string", };
            summary.Response<ForgotPasswordHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = ForgotPasswordResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<ForgotPasswordHttpResponse> ExecuteAsync(
        ForgotPasswordRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = ForgotPasswordHttpResponseMapper
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
