using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Auths.Login.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Users.GetProfileUser.Common;
using Clinic.WebAPI.EndPoints.Users.GetProfileUser.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.GetProfileUser;

/// <summary>
///     Login endpoint.
/// </summary>
internal sealed class GetProfileUserEndpoint : Endpoint<EmptyRequest, GetProfileUserHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "user/profile");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for User feature";
            summary.Description = "This endpoint is used for display profile user.";
            summary.Response<GetProfileUserHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetProfileUserHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var stateBag = ProcessorState<GetProfileUserStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetProfileUserHttpResponseMapper
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
