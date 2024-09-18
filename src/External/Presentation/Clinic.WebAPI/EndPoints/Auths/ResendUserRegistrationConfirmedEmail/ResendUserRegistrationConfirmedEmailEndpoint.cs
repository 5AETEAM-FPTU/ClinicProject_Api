using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.ResendUserRegistrationConfirmedEmail;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.ResendUserRegistrationConfirmedEmail.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail endpoint.
/// </summary>
internal sealed class ResendUserRegistrationConfirmedEmailEndpoint
    : Endpoint<
        ResendUserRegistrationConfirmedEmailRequest,
        ResendUserRegistrationConfirmedEmailHttpResponse
    >
{
    public override void Configure()
    {
        Post(routePatterns: "auth/resend-email-confirmation");
        DontThrowIfValidationFails();
        AllowAnonymous();
        PreProcessor<ValidationPreProcessor<ResendUserRegistrationConfirmedEmailRequest>>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for resend user registration confirmed email.";
            summary.Description =
                "This endpoint is used for resend user registration confirmed email purpose.";
            summary.ExampleRequest = new() { Email = "string", };
            summary.Response<ResendUserRegistrationConfirmedEmailHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        ResendUserRegistrationConfirmedEmailResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<ResendUserRegistrationConfirmedEmailHttpResponse> ExecuteAsync(
        ResendUserRegistrationConfirmedEmailRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = ResendUserRegistrationConfirmedEmailHttpResponseMapper
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
