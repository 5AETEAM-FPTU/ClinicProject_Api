using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

internal sealed class UpdateAppointmentDepositPaymentEndpoint:Endpoint<UpdateAppoinmentDepositPaymenRequest, UpdateAppointmentDepositPaymentHttpResponse> {

    public override void Configure() {

        base.Configure();
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