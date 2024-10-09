
namespace Clinic.Application.Commons.VNPay.Config;

public class VnpayConfig
{

    public static string ConfigName => "Vnpay";
    public string Version { get; set; } = string.Empty;
    public string TmnCode { get; set; } = string.Empty;
    public string HashSecret { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
    public string PaymentUrl { get; set;} = string.Empty;
}
