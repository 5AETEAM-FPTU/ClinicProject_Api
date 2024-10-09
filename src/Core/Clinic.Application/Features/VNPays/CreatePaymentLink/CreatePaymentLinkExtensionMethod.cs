
namespace Clinic.Application.Features.VNPays.CreatePaymentLink;

/// <summary>
///     Extension Method for GetAllDoctors features.
/// </summary>
public static class CreatePaymentLinkExtensionMethod
{
    public static string ToAppCode(this CreatePaymentLinkResponseStatusCode statusCode)
    {
        return $"{nameof(CreatePaymentLink)}Feature: {statusCode}";
    }
}