using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     HandleRedirectURL Request
/// </summary>
public class HandleRedirectURLRequest : IFeatureRequest<HandleRedirectURLResponse>
{
    [BindFrom("vnp_Amount")]
    public string Amount { get; set; }

    [BindFrom("vnp_BankCode")]
    public string BankCode { get; set; }

    [BindFrom("vnp_BankTranNo")]
    public string BankTransactionNo { get; set; }

    [BindFrom("vnp_CardType")]
    public string CardType { get; set; }

    [BindFrom("vnp_OrderInfo")]
    public string OrderInfo { get; set; }

    [BindFrom("vnp_PayDate")]
    public string PayDate { get; set; }

    [BindFrom("vnp_ResponseCode")]
    public string ResponseCode { get; set; }

    [BindFrom("vnp_TmnCode")]
    public string TerminalCode { get; set; }

    [BindFrom("vnp_TransactionNo")]
    public string TransactionNo { get; set; }

    [BindFrom("vnp_TransactionStatus")]
    public string TransactionStatus { get; set; }

    [BindFrom("vnp_TxnRef")]
    public string TransactionRef { get; set; }

    [BindFrom("vnp_SecureHash")]
    public string SecureHash { get; set; }
}
