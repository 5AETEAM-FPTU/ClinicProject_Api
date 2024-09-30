using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.OnlinePayment.CreateNewOnlinePayment;

internal sealed class CreateNewOnlinePaymentHandler
    : IFeatureHandler<CreateNewOnlinePaymentRequest, CreateNewOnlinePaymentResponse>
{
    public async Task<CreateNewOnlinePaymentResponse> ExecuteAsync(
        CreateNewOnlinePaymentRequest command,
        CancellationToken ct
    )
    {
        return new() {
            StatusCode = CreateNewOnlinePaymentResponseStatusCode.OPERATION_SUCCESS
        };
    }
}
