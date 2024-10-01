using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.ChangingPassword;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.ChangingPassword.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ChangingPassword;

/// <summary>
///     ChangingPassword endpoint.
/// </summary>
internal sealed class ChangingPasswordEndpoint
    : Endpoint<ChangingPasswordRequest, ChangingPasswordHttpResponse>
{
    public override void Configure()
    {
        Patch(routePatterns: "auth/changing-password");
        PreProcessor<ValidationPreProcessor<ChangingPasswordRequest>>();
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for changing password by inputting otp.";
            summary.Description = "This endpoint is used for changing password purpose.";
            summary.ExampleRequest = new()
            {
                NewPassword = "string",
                ResetPasswordToken = "string",
                Email = "string",
            };
            summary.Response<ChangingPasswordHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = ChangingPasswordResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<ChangingPasswordHttpResponse> ExecuteAsync(
        ChangingPasswordRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = ChangingPasswordHttpResponseMapper
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
