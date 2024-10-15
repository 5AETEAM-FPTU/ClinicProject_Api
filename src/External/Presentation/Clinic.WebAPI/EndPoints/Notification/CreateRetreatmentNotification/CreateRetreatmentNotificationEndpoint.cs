using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Notification.CreateRetreatmentNotification;
using Clinic.WebAPI.EndPoints.Notification.CreateRetreatmentNotification.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Notification.CreateRetreatmentNotification;

/// <summary>
///     CreateRetreatmentNotification endpoint
/// </summary>
public class CreateRetreatmentNotificationEndpoint
    : Endpoint<CreateRetreatmentNotificationRequest, CreateRetreatmentNotificationHttpResponse>
{
    public override void Configure()
    {
        Post("notification/retreatment/create");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to create retreatment notification, send via SMS";
            summary.Description =
                "This endpoint allows doctor/staff for creating retreatment notification, send via SMS.";
            summary.Response<CreateRetreatmentNotificationHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        CreateRetreatmentNotificationResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateRetreatmentNotificationHttpResponse> ExecuteAsync(
        CreateRetreatmentNotificationRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = CreateRetreatmentNotificationHttpResponseMapper
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
