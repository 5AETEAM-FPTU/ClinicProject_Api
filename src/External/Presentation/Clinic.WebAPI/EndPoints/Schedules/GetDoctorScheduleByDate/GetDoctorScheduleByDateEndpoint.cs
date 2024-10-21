using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;
using Clinic.WebAPI.EndPoints.Schedules.GetDoctorScheduleByDate.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorScheduleByDate;

public class GetDoctorScheduleByDateEndpoint
    : Endpoint<GetDoctorScheduleByDateRequest, GetDoctorScheduleByDateHttpResponse>
{
    public override void Configure()
    {
        Get("/schedules/guest/date");
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for guest overview to get doctor schedule in date.";
            summary.Description = "This endpoint allows guest to get doctor schedule in date";
            summary.Response<GetDoctorScheduleByDateHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        GetDoctorScheduleByDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetDoctorScheduleByDateHttpResponse> ExecuteAsync(
        GetDoctorScheduleByDateRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetDoctorScheduleByDateHttpResponseMapper
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
