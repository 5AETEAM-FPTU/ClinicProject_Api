using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Doctors.GetAppointmentsByDate;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Doctors.GetAppointmentsByDate.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetSchedulesByDate endpoint
/// </summary>
public class GetAppointmentsByDateEndpoint
    : Endpoint<GetAppointmentsByDateRequest, GetAppointmentsByDateHttpResponse>
{
    public override void Configure()
    {
        Get("doctor/appointments/{date}");
        PreProcessor<ValidationPreProcessor<GetAppointmentsByDateRequest>>();
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to get appointments on date of doctor.";
            summary.Description = "This endpoint allows doctor to get his appointments on date.";
            summary.Response<GetAppointmentsByDateHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAppointmentsByDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetAppointmentsByDateHttpResponse> ExecuteAsync(
        GetAppointmentsByDateRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAppointmentByDateHttpResponseMapper
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
