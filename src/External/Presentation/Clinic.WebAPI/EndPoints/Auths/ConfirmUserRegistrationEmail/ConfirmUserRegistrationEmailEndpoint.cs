using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.ConfirmUserRegistrationEmail.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail endpoint.
/// </summary>
internal sealed class ConfirmUserRegistrationEmailEndpoint
    : Endpoint<ConfirmUserRegistrationEmailRequest, ConfirmUserRegistrationEmailHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "auth/confirm-email");
        PreProcessor<ValidationPreProcessor<ConfirmUserRegistrationEmailRequest>>();
        AllowAnonymous();
        DontThrowIfValidationFails();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for confirming password by email.";
            summary.Description = "This endpoint is used for confirming password by email purpose.";
            summary.ExampleRequest = new()
            {
                UserRegistrationEmailConfirmedTokenAsBase64 = "string"
            };
            summary.Response<ConfirmUserRegistrationEmailHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        ConfirmUserRegistrationEmailResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<ConfirmUserRegistrationEmailHttpResponse> ExecuteAsync(
        ConfirmUserRegistrationEmailRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = ConfirmUserRegistrationEmailHttpResponseMapper
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
