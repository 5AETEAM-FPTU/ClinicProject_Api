using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Doctors.GetAppointmentsByDate;
using Clinic.Application.Features.Doctors.GetRecentBookedAppointments;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Doctors.GetAppointmentsByDate.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Doctors.GetRecentBookedAppointments.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentBookedAppointments;

/// <summary>
///     GetAppointmentsByDate endpoint
/// </summary>
public class GetRecentBookedAppointmentsEndpoint
    : Endpoint<GetRecentBookedAppointmentsRequest, GetRecentBookedAppointmentsHttpResponse>
{
    public override void Configure()
    {
        Get("/doctor/appointments/recent");
        PreProcessor<ValidationPreProcessor<GetRecentBookedAppointmentsRequest>>();
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor/staff.";
            summary.Description = "This endpoint allows doctor to get his recent booked appointments.";
            summary.Response<GetRecentBookedAppointmentsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAppointmentsByDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetRecentBookedAppointmentsHttpResponse> ExecuteAsync(
        GetRecentBookedAppointmentsRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetRecentBookedAppointmentsHttpResponseMapper
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
