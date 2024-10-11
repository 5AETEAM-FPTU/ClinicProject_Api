namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     Extension Method for HandleRedirectURL features.
/// </summary>
public static class HandleRedirectURLExtensionMethod
{
    public static string ToAppCode(this HandleRedirectURLResponseStatusCode statusCode)
    {
        return $"{nameof(HandleRedirectURL)}Feature: {statusCode}";
    }
}
