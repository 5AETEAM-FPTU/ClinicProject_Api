using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Doctors.GetAppointmentsByDate.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Schedules.GetScheduleDatesByMonth.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate endpoint
/// </summary>
public class GetScheduleDatesByMonthEndpoint
    : Endpoint<GetScheduleDatesByMonthRequest, GetScheduleDatesByMonthHttpResponse>
{
    public override void Configure()
    {
        Get("/schedules/month");
        PreProcessor<ValidationPreProcessor<GetScheduleDatesByMonthRequest>>();
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor overview to get date they have schedule.";
            summary.Description = "This endpoint allows doctor to get schedule dates by month.";
            summary.Response<GetScheduleDatesByMonthHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetScheduleDatesByMonthResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetScheduleDatesByMonthHttpResponse> ExecuteAsync(
        GetScheduleDatesByMonthRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetScheduleDatesByMonthHttpResponseMapper
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
