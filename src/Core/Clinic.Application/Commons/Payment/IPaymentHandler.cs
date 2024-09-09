using System.Threading.Tasks;

namespace Clinic.Application.Commons.Payment;

/// <summary>
///     Represent interface of payment gateway handler.
/// </summary>
public interface IPaymentHandler
{
    /// <summary>
    ///     Create payment link url.
    /// </summary>
    /// <param name="paymentData">
    ///     Model contains payment information.
    /// </param>
    /// <returns>
    ///     String contain checkout url.
    /// </returns>
    Task<string> CreatePaymentLink(PaymentModel paymentData);

    /// <summary>
    ///     Verify webhook data signature.
    /// </summary>
    /// <param name="paymentData">
    ///     Model contains payment information.
    /// </param>
    /// <returns>
    ///     return webhooktype model.
    /// </returns>
    bool VerifyWebhookData(WebhookType webhookType);
}
