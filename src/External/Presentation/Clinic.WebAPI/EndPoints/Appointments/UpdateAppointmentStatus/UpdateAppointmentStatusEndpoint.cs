using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

/// <summary>
/// UpdateAppointmentStatusEndpoint
/// </summary>
internal class UpdateAppointmentStatusEndpoint
    : Endpoint<UpdateAppointmentStatusRequest, UpdateAppointmentStatusHttpResponse>
{
    public override void Configure()
    {
        Patch("appointment/update-status");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for doctor/staff update appointment status.";
            summary.Description = "This endpoint is used for update appointment status purpose.";
            summary.Response<UpdateAppointmentStatusHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        UpdateAppointmentStatusResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateAppointmentStatusHttpResponse> ExecuteAsync(
        UpdateAppointmentStatusRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct);

        var httpResponse = UpdateAppointmentStatusHttpReponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct
        );

        httpResponse.HttpCode = httpResponseStatusCode;
        return httpResponse;
    }
}
