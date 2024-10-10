using Clinic.VNPAY.Common;

namespace Clinic.VNPAY.Model;

/// <summary>
///     Represents a VNPay payment request.
/// </summary>
internal class VNPayRequest
{
    public string Version { get; set; } = "2.1.0";
    public string Command { get; set; } = "pay";
    public string TmnCode { get; set; } = "V5W1IC41";
    public int? Amount { get; set; }
    public string BankCode { get; set; } = "VNPAY";
    public string CreateDate { get; set; }
    public string CurrCode { get; set; } = "VND";
    public string IpAddr { get; set; }
    public string Locale { get; set; } = "vn";
    public string OrderInfo { get; set; }
    public string OrderType { get; set; } = "other";
    public string ReturnUrl { get; set; }
    public string ExpireDate { get; set; }
    public string TxnRef { get; set; }
    public string SecureHash { get; set; }

    public SortedList<string, string> RequestData { get; private set; } =
        new SortedList<string, string>(new VNPayCompare());

    public VNPayRequest()
    {
        RequestData = new SortedList<string, string>(new VNPayCompare());
    }

    public VNPayRequest(
        string ipAddr,
        int amount,
        string orderInfo,
        DateTime createdDate,
        string txnRef,
        string appointmentId
    )
        : this()
    {
        IpAddr = ipAddr;
        Amount = amount * 100; // Amount in VND
        OrderInfo = orderInfo;
        CreateDate = createdDate.ToString("yyyyMMddHHmmss");
        TxnRef = txnRef;
        ReturnUrl =
            $"https://localhost:7161/payment/return-url/success?appointmentId={appointmentId}";
    }

    public void MakeRequestData()
    {
        AddRequestData("vnp_Amount", Amount?.ToString());
        AddRequestData("vnp_Command", Command);
        AddRequestData("vnp_CreateDate", CreateDate);
        AddRequestData("vnp_CurrCode", CurrCode);
        AddRequestData("vnp_BankCode", BankCode);
        AddRequestData("vnp_IpAddr", IpAddr);
        AddRequestData("vnp_Locale", Locale);
        AddRequestData("vnp_OrderInfo", OrderInfo);
        AddRequestData("vnp_OrderType", OrderType);
        AddRequestData("vnp_ReturnUrl", ReturnUrl);
        AddRequestData("vnp_TmnCode", TmnCode);
        AddRequestData("vnp_ExpireDate", ExpireDate);
        AddRequestData("vnp_TxnRef", TxnRef);
        AddRequestData("vnp_Version", Version);
    }

    private void AddRequestData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            RequestData.Add(key, value);
        }
    }
}
