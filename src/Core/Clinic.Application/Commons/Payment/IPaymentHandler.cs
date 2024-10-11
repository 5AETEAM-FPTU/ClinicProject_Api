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
    string CreatePaymentLink(PaymentModel paymentData);

    /// <summary>
    ///     Verify IPN
    /// </summary>
    /// <param name="webhookType">
    ///     Model contain ipn data.
    /// </param>
    /// <returns></returns>
    bool VerifySignatureForIPN(WebhookType webhookType);

    /// <summary>
    ///     Verify secure key
    /// </summary>
    /// <param name="secureHash"></param>
    /// <returns></returns>
    bool VerifySecureKey(string secureHash);
}
