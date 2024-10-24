namespace Clinic.Application.Features.Admin.GetStaticInformation;

/// <summary>
///     Extension Method for GetStaticInformation features.
/// </summary>
public static class GetStaticInformationExtensionMethod
{
    public static string ToAppCode(this GetStaticInformationResponseStatusCode statusCode)
    {
        return $"{nameof(GetStaticInformation)}Feature: {statusCode}";
    }
}
