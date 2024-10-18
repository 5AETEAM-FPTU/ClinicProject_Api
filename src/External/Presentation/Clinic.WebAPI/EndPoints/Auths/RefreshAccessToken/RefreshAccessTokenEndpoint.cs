using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.RefreshAccessToken;
using Clinic.WebAPI.Commons.Behaviors.Authorization;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.RefreshAccessToken.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken endpoint.
/// </summary>
internal sealed class RefreshAccessTokenEndpoint
    : Endpoint<RefreshAccessTokenRequest, RefreshAccessTokenHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/refresh-access-token");
        DontThrowIfValidationFails();
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<RefreshAccessTokenRequest>>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for refreshing access token";
            summary.Description = "This endpoint is used for refreshing access token purpose.";
            summary.ExampleRequest = new() { RefreshToken = "string" };
            summary.Response<RefreshAccessTokenHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new RefreshAccessTokenResponse.Body()
                    {
                        AccessToken = "string",
                        RefreshToken = "string",
                    }
                }
            );
        });
    }

    public override async Task<RefreshAccessTokenHttpResponse> ExecuteAsync(
        RefreshAccessTokenRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = RefreshAccessTokenHttpResponseMapper
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
