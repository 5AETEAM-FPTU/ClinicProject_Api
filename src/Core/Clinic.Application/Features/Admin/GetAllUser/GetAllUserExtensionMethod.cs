namespace Clinic.Application.Features.Admin.GetAllUser;

/// <summary>
///     Extension Method for GetAllUser features.
/// </summary>
public static class GetAllUserExtensionMethod
{
    public static string ToAppCode(this GetAllUserResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllUser)}Feature: {statusCode}";
    }
}