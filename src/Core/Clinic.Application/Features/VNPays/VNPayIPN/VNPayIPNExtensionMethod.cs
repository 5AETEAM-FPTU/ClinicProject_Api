
using Clinic.Application.Commons.VNPay.Request;
using Clinic.Application.Commons.VNPay.Response;

namespace Clinic.Application.Features.VNPays.VNPayIPN;

/// <summary>
///     Extension Method for GetAllDoctors features.
/// </summary>
public static class CreatePaymentLinkExtensionMethod
{
    public static string ToAppCode(this VNPayIPNResponseStatusCode statusCode)
    {
        return $"{nameof(VNPayIPN)}Feature: {statusCode}";
    }
}