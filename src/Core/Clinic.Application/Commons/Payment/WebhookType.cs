namespace Clinic.Application.Commons.Payment;

/// <summary>
///     Represent the webhook data input model.
/// </summary>
public class WebhookType
{
    public string TmnCode { get; set; }

    public string BankCode { get; set; }

    public string BankTranNo { get; set; }

    public string CardType { get; set; }

    public string OrderInfo { get; set; }

    public string TransactionNo { get; set; }

    public string TransactionStatus { get; set; }

    public string TxnRef { get; set; }

    public string SecureHashType { get; set; }

    public string SecureHash { get; set; }

    public int? Amount { get; set; }

    public string ResponseCode { get; set; }

    public string PayDate { get; set; }

    public string AppointmentId { get; set; }
}
