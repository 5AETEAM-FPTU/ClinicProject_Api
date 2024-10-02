using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

internal sealed class UpdateAppointmentDepositPaymentEndpoint:Endpoint<UpdateAppoinmentDepositPaymenRequest, UpdateAppointmentDepositPaymentHttpResponse> {

    public override void Configure() {

        Patch("appointment/update-deposit-payment");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for update appointment deposit payment.";
            summary.Description = "This endpoint is used for update appointment deposit payment purpose.";
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

    public override async Task<UpdateAppointmentDepositPaymentHttpResponse> ExecuteAsync(UpdateAppoinmentDepositPaymenRequest req, CancellationToken ct)
    {
        var appResponse = await req.ExecuteAsync(ct);
        var httpResponse = UpdateAppointm1entDepositPaymentHttpResponseMapper.Get().Resolve(statusCode: appResponse.StatusCode).Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(response: httpResponse, statusCode: httpResponseStatusCode, cancellation: ct);

        httpResponse.HttpCode = httpResponseStatusCode;
        return httpResponse;
    }

}