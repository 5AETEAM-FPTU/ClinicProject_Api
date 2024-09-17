namespace Clinic.Application.Commons.Abstractions.GetProfileUser;

/// <summary>
///     Extension Method for GetProfileUser features.
/// </summary>
public static class GetProfileUserExtensionMethod
{
    public static string ToAppCode(this GetProfileUserResponseStatusCode statusCode)
    {
        return $"{nameof(GetProfileUser)}Feature: {statusCode}";
    }
}
