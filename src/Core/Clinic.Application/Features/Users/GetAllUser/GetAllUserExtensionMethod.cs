
namespace Clinic.Application.Features.Users.GetAllUser;

/// <summary>
///     Extension Method for GetAllDoctors features.
/// </summary>
public static class GetAllUserExtensionMethod
{
    public static string ToAppCode(this GetAllUserResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllUser)}Feature: {statusCode}";
    }
}