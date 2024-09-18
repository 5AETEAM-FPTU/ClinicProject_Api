using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Users.UpdatePasswordUser;
using Clinic.WebAPI.Commons.Behaviors.Authorization;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Users.UpdatePasswordUser.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.UpdatePasswordUser;

/// <summary>
///     UpdatePasswordUser endpoint.
/// </summary>
internal sealed class UpdatePasswordUserEndpoint
    : Endpoint<UpdatePasswordUserRequest, UpdatePasswordUserHttpResponse>
{
    public override void Configure()
    {
        Patch(routePatterns: "auth/update-password");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<UpdatePasswordUserRequest>>();
        PreProcessor<AuthorizationPreProcessor<UpdatePasswordUserRequest>>();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for updating password.";
            summary.Description =
                "This endpoint is used for updating password with oldpassword purpose.";
            summary.ExampleRequest = new() { NewPassword = "string", CurrentPassword = "string", };
            summary.Response<UpdatePasswordUserHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdatePasswordUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdatePasswordUserHttpResponse> ExecuteAsync(
        UpdatePasswordUserRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = UpdatePasswordUserHttpResponseMapper
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
