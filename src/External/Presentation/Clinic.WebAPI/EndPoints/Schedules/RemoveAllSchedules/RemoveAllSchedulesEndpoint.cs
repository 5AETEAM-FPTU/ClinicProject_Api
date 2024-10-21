using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Schedules.RemoveAllSchedules;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Schedules.RemoveAllSchedules.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.RemoveAllSchedules;

/// <summary>
///     UpdateSchedule endpoint
/// </summary>
public class RemoveScheduleEndpoint
    : Endpoint<RemoveAllSchedulesRequest, RemoveAllSchedulesHttpResponse>
{
    public override void Configure()
    {
        Delete("schedules/remove/all");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<RemoveAllSchedulesRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor";
            summary.Description = "This endpoint allows doctor for remove all schedule on date.";
            summary.Response<RemoveAllSchedulesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RemoveAllSchedulesResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemoveAllSchedulesHttpResponse> ExecuteAsync(
        RemoveAllSchedulesRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveAllSchedulesHttpResponseMapper
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
