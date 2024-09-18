using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.RegisterAsUser;
using Clinic.WebAPI.Commons.Behaviors.Authorization;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Auths.RegisterAsUser.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.RegisterAsUser;

/// <summary>
///     RegisterAsUser endpoint.
/// </summary>
internal sealed class RegisterAsUserEndpoint
    : Endpoint<RegisterAsUserRequest, RegisterAsUserHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/register-user");
        DontThrowIfValidationFails();
        AllowAnonymous();
        PreProcessor<ValidationPreProcessor<RegisterAsUserRequest>>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for register patient acoount";
            summary.Description = "This endpoint is used for registering patient account purpose.";
            summary.ExampleRequest = new()
            {
                Email = "string",
                FullName = "string",
                Password = "string",
                PhoneNumber = "string",
            };
            summary.Response<RegisterAsUserHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RegisterAsUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RegisterAsUserHttpResponse> ExecuteAsync(
        RegisterAsUserRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = RegisterAsUserHttpResponseMapper
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
