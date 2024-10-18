using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.Doctors.GetUserNotification;
using Clinic.WebAPI.EndPoints.Doctors.GetUserNotification.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUserNotification;

/// <summary>
///     GetUserNotification endpoint.
/// </summary>
internal sealed class GetUserNotificationEndpoint
    : Endpoint<GetUserNotificationRequest, GetUserNotificationHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "notification/user/up-comming");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Doctor/Staff feature";
            summary.Description =
                "This endpoint is used for display user up-comming retreatment notification.";
            summary.Response<GetUserNotificationHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetUserNotificationHttpResponse> ExecuteAsync(
        GetUserNotificationRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetUserNotificationHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(httpResponse, httpResponseStatusCode, ct);

        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
