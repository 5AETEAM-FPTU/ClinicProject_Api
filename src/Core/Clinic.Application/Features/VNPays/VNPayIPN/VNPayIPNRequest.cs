using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.VNPay.Response;
using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.VNPays.VNPayIPN;

public class VNPayIPNRequest : IFeatureRequest<VnpayPayIpnResponse>
{
    [BindFrom("vnp_TmnCode")]
    public string vnp_TmnCode { get; set; } = string.Empty;

    [BindFrom("vnp_BankCode")]
    public string vnp_BankCode { get; set; } = string.Empty;

    [BindFrom("vnp_BankTranNo")]
    public string vnp_BankTranNo { get; set; } = string.Empty;

    [BindFrom("vnp_CardType")]
    public string vnp_CardType { get; set; } = string.Empty;

    [BindFrom("vnp_OrderInfo")]
    public string vnp_OrderInfo { get; set; } = string.Empty;

    [BindFrom("vnp_TransactionNo")]
    public string vnp_TransactionNo { get; set; } = string.Empty;

    [BindFrom("vnp_TransactionStatus")]
    public string vnp_TransactionStatus { get; set; } = string.Empty;

    [BindFrom("vnp_TxnRef")]
    public string vnp_TxnRef { get; set; } = string.Empty;

    [BindFrom("vnp_SecureHashType")]
    public string vnp_SecureHashType { get; set; } = string.Empty;

    [BindFrom("vnp_SecureHash")]
    public string vnp_SecureHash { get; set; } = string.Empty;

    [BindFrom("vnp_Amount")]
    public int? vnp_Amount { get; set; }

    [BindFrom("vnp_ResponseCode")]
    public string vnp_ResponseCode { get; set; }

    [BindFrom("vnp_PayDate")]
    public string vnp_PayDate { get; set; } = string.Empty;

    [BindFrom("appointmentId")]
    public string appointmentId { get; set; } = string.Empty;
}
