using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Schedules.RemoveSchedule;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Schedules.RemoveSchedule.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.RemoveSchedule;

/// <summary>
///     UpdateSchedule endpoint
/// </summary>
public class RemoveScheduleEndpoint : Endpoint<RemoveScheduleRequest, RemoveScheduleHttpResponse>
{
    public override void Configure()
    {
        Delete("schedules/remove/{scheduleId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<RemoveScheduleRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor";
            summary.Description =
                "This endpoint allows doctor for remove specific schedule.";
            summary.Response<RemoveScheduleHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RemoveScheduleResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemoveScheduleHttpResponse> ExecuteAsync(
        RemoveScheduleRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveScheduleHttpResponseMapper
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
