using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.LoginWithGoogle;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.LoginWithGoogle.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.LoginWithGoogle;

/// <summary>
///     LoginWithGoogle endpoint.
/// </summary>
internal sealed class LoginWithGoogleEndpoint
    : Endpoint<LoginWithGoogleRequest, LoginWithGoogleHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/login/google");
        PreProcessor<ValidationPreProcessor<LoginWithGoogleRequest>>();
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Login with google feature";
            summary.Description = "This endpoint is used for Login with google api purpose.";
            summary.ExampleRequest = new() { IdToken = "string" };
            summary.Response<LoginWithGoogleHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginWithGoogleResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new LoginWithGoogleResponse.Body()
                    {
                        AccessToken = "string",
                        RefreshToken = "string",
                        User = new()
                        {
                            AvatarUrl = "string",
                            FullName = "string",
                            Email = "string",
                        }
                    }
                }
            );
        });
    }

    public override async Task<LoginWithGoogleHttpResponse> ExecuteAsync(
        LoginWithGoogleRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LoginWithGoogleHttpResponseMapper
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
