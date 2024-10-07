using Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Appointments.UpdateUserBookedAppointment.HttpResponseMapper;
using Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;
using FastEndpoints;
using Microsoft.AspNetCore.Http;


namespace Clinic.WebAPI.EndPoints.Appointments.UpdateUserBookedAppointment;

internal sealed class UpdateUserBookedAppointmentEndpoint : Endpoint<UpdateUserBookedAppointmentRequest, UpdateUserBookedAppointmentHttpResponse>
{
    public override void Configure()
    {
        Patch("appointment/user-update");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for user to update their booked appointment.";
            summary.Description = "This endpoint is used for update their booked appointment purpose.";
            summary.Response<UpdateAppointmentDepositPaymentHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateAppointmentDepositPaymentResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<UpdateUserBookedAppointmentHttpResponse> ExecuteAsync(UpdateUserBookedAppointmentRequest req, CancellationToken ct)
    {
        var appResponse = await req.ExecuteAsync(ct);
        var httpResponse = UpdateUserBookedAppointmentHttpResponseMapper.Get().Resolve(statusCode: appResponse.StatusCode).Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(response: httpResponse, statusCode: httpResponseStatusCode, cancellation: ct);

        httpResponse.HttpCode = httpResponseStatusCode;
        return httpResponse;
    }
}
