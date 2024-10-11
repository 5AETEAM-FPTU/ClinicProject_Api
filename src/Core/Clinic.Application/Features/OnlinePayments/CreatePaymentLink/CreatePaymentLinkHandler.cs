using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Payment;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.OnlinePayments.CreatePaymentLink;

/// <summary>
///     CreatePaymentLink Handler
/// </summary>
public class CreatePaymentLinkHandler
    : IFeatureHandler<CreatePaymentLinkRequest, CreatePaymentLinkResponse>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IPaymentHandler _paymentHandler;

    public CreatePaymentLinkHandler(
        IHttpContextAccessor contextAccessor,
        IPaymentHandler paymentHandler
    )
    {
        _contextAccessor = contextAccessor;
        _paymentHandler = paymentHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<CreatePaymentLinkResponse> ExecuteAsync(
        CreatePaymentLinkRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get ip address
        var ipAddress = _contextAccessor?.HttpContext?.Connection?.LocalIpAddress?.ToString();

        var paymentModel = new PaymentModel()
        {
            IPAddress = ipAddress,
            Amount = request.Amount,
            OrderInfo = request.Description,
            AppointmentId = request.AppointmentId.ToString(),
            CreatedDate = DateTime.Now,
            TxnRef = Guid.NewGuid().ToString(),
        };

        var paymentUrl = _paymentHandler.CreatePaymentLink(paymentModel);

        return new CreatePaymentLinkResponse()
        {
            StatusCode = CreatePaymentLinkResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { PaymentUrl = paymentUrl, }
        };
    }
}
