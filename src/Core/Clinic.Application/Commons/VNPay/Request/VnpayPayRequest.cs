using Clinic.Application.Commons.VNPay.Helpers;
using Clinic.Application.Commons.VNPay.Config;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Clinic.Application.Commons.VNPay.Lib;

namespace Clinic.Application.Commons.VNPay.Request;

public class VnpayPayRequest
{
    public string vnp_Version { get; set; }
    public string vnp_Command { get; set; }
    public string vnp_TmnCode { get; set; }
    public int? vnp_Amount { get; set; }
    public string vnp_BankCode { get; set; }
    public string vnp_CreateDate { get; set; }
    public string vnp_CurrCode { get; set; }
    public string vnp_IpAddr { get; set; }
    public string vnp_Locale { get; set; }
    public string vnp_OrderInfo { get; set; }
    public string vnp_OrderType { get; set; }
    public string vnp_ReturnUrl { get; set; }
    public string vnp_ExpireDate { get; set; }
    public string vnp_TxnRef { get; set; }    
    public string vnp_SecureHash { get; set; }


    public SortedList<string, string> requestData 
        = new SortedList<string, string>(new VnpayCompare());
    public VnpayPayRequest() { }
    public VnpayPayRequest(string vnp_IpAddr, int vnp_Amount, string vnp_OrderInfo, DateTime createdDate, string vnp_TxnRef, string appointmentId, string userId)
    {
        this.vnp_Version = "2.1.0";
        this.vnp_Command = "pay";
        this.vnp_TmnCode = "V5W1IC41";                                         // có sẵn config
        this.vnp_Amount = vnp_Amount * 100;
        this.vnp_BankCode = "VNPAY";
        this.vnp_CreateDate = createdDate.ToString("yyyyMMddHHmmss");
        this.vnp_CurrCode = "VND";
        this.vnp_IpAddr = vnp_IpAddr;
        this.vnp_Locale = "vn";
        this.vnp_OrderInfo = vnp_OrderInfo;
        this.vnp_OrderType = "other";
        this.vnp_ReturnUrl = $"http://localhost:3000/vi/user/treatment-calendar/booking/success?appointmentId={appointmentId}";          // có sẵn config
        this.vnp_TxnRef = vnp_TxnRef;                                           // id online payment
    }

    public string GetLink(string baseUrl, string secretKey)
    {
        MakeRequestData();
        StringBuilder data = new StringBuilder();
        foreach(KeyValuePair<string, string> kv in requestData)
        {
            if(!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            }
        }

        string result = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html" + "?" + data.ToString();           // base url
        var secureHash = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
        return result += "vnp_SecureHash=" + secureHash;
    }

    public void MakeRequestData()
    {
        if (vnp_Amount != null)
            requestData.Add("vnp_Amount", vnp_Amount.ToString() ?? string.Empty);
        if (vnp_Command != null)
            requestData.Add("vnp_Command", vnp_Command);
        if (vnp_CreateDate != null)
            requestData.Add("vnp_CreateDate", vnp_CreateDate);
        if (vnp_CurrCode != null)
            requestData.Add("vnp_CurrCode", vnp_CurrCode);
        if (vnp_BankCode != null)
            requestData.Add("vnp_BankCode", vnp_BankCode);
        if (vnp_IpAddr != null)
            requestData.Add("vnp_IpAddr", vnp_IpAddr);
        if (vnp_Locale != null)
            requestData.Add("vnp_Locale", vnp_Locale);
        if (vnp_OrderInfo != null)
            requestData.Add("vnp_OrderInfo", vnp_OrderInfo);
        if (vnp_OrderType != null)
            requestData.Add("vnp_OrderType", vnp_OrderType);
        if (vnp_ReturnUrl != null)
            requestData.Add("vnp_ReturnUrl", vnp_ReturnUrl);
        if (vnp_TmnCode != null)
            requestData.Add("vnp_TmnCode", vnp_TmnCode);
        if (vnp_ExpireDate != null)
            requestData.Add("vnp_ExpireDate", vnp_ExpireDate);
        if (vnp_TxnRef != null)
            requestData.Add("vnp_TxnRef", vnp_TxnRef);
        if (vnp_Version != null)
            requestData.Add("vnp_Version", vnp_Version);
    }
}
