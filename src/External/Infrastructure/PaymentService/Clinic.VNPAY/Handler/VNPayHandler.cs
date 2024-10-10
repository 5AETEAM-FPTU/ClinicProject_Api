using System.Net;
using System.Text;
using Clinic.Application.Commons.Payment;
using Clinic.Application.Commons.Utils;
using Clinic.Configuration.Infrastructure.Payment.VNPay;
using Clinic.VNPAY.Model;

namespace Clinic.VNPAY.Handler;

/// <summary>
///     VNPay handler implementation IPaymentHandler.
/// </summary>
internal class VNPayHandler : IPaymentHandler
{
    private VNPayOption _options;

    public VNPayHandler(VNPayOption option)
    {
        _options = option;
    }

    public string CreatePaymentLink(PaymentModel paymentData)
    {
        var vnpayRequest = new VNPayRequest(
            ipAddr: paymentData.IPAddress,
            amount: paymentData.Amount,
            orderInfo: paymentData.OrderInfo,
            createdDate: paymentData.CreatedDate,
            txnRef: paymentData.TxnRef,
            appointmentId: paymentData.AppointmentId
        );

        vnpayRequest.MakeRequestData();

        StringBuilder data = new StringBuilder();

        foreach (KeyValuePair<string, string> kv in vnpayRequest.RequestData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(
                    WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&"
                );
            }
        }

        string result =
            "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html" + "?" + data.ToString();
        var secureHash = HashHelper.HmacSHA512(
            _options.SecretKey,
            data.ToString().Remove(data.Length - 1, 1)
        );

        return result += "vnp_SecureHash=" + secureHash;
    }

    public bool VerifySignatureForIPN(WebhookType webhookType)
    {
        var vNPayRepsonse = new VNPayResponse(
            webhookType.TmnCode,
            webhookType.BankCode,
            webhookType.BankTranNo,
            webhookType.CardType,
            webhookType.OrderInfo,
            webhookType.TransactionNo,
            webhookType.TransactionStatus,
            webhookType.TxnRef,
            webhookType.SecureHashType,
            webhookType.SecureHash,
            webhookType.Amount,
            webhookType.ResponseCode,
            webhookType.PayDate,
            webhookType.AppointmentId
        );

        vNPayRepsonse.MakeResponseData();

        StringBuilder data = new StringBuilder();

        foreach (KeyValuePair<string, string> kv in vNPayRepsonse.responseData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(
                    WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&"
                );
            }
        }
        string checkSum = HashHelper.HmacSHA512(_options.SecretKey, data.ToString().TrimEnd('&'));

        return checkSum.Equals(
            vNPayRepsonse.vnp_SecureHash,
            StringComparison.InvariantCultureIgnoreCase
        );
    }
}
