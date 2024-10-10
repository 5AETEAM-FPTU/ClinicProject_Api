using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Payment;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     HandleRedirectURL Handler
/// </summary>
public class HandleRedirectURLHandler
    : IFeatureHandler<HandleRedirectURLRequest, HandleRedirectURLResponse>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IPaymentHandler _paymentHandler;

    public HandleRedirectURLHandler(
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
    public async Task<HandleRedirectURLResponse> ExecuteAsync(
        HandleRedirectURLRequest request,
        CancellationToken cancellationToken
    )
    {
        return new HandleRedirectURLResponse()
        {
            StatusCode = HandleRedirectURLResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
