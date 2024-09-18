using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.LoginByAdmin;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.LoginByAdmin.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.LoginByAdmin;

/// <summary>
///     LoginByAdmin endpoint.
/// </summary>
internal sealed class LoginByAdminEndpoint : Endpoint<LoginByAdminRequest, LoginByAdminHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/login/admin");
        PreProcessor<ValidationPreProcessor<LoginByAdminRequest>>();
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Login by admin feature";
            summary.Description = "This endpoint is used for Login by admin purpose.";
            summary.ExampleRequest = new() { Username = "string", Password = "string", };
            summary.Response<LoginByAdminHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginByAdminResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new LoginByAdminResponse.Body()
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

    public override async Task<LoginByAdminHttpResponse> ExecuteAsync(
        LoginByAdminRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LoginByAdminHttpResponseMapper
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
