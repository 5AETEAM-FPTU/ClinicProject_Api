using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;
using Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;
using Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Schedules.GetScheduleDatesByMonth.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Schedules.GetSchedulesByDate.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate;

public class GetDoctorMonthlyDateEndpoint
    : Endpoint<GetDoctorMonthlyDateRequest, GetDoctorMonthlyDateHttpResponse>
{
    public override void Configure()
    {
        Get("/schedules/guest/month");
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for guest overview to get date they have schedule.";
            summary.Description = "This endpoint allows guest to get schedule dates by month.";
            summary.Response<GetDoctorMonthlyDateHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetDoctorMonthlyDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetDoctorMonthlyDateHttpResponse> ExecuteAsync(
        GetDoctorMonthlyDateRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetDoctorMonthlyDateHttpResponseMapper
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
