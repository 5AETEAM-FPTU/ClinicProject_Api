namespace Clinic.Application.Features.Doctors.GetUserInforById;


/// <summary>
///     Extension Method for GetUserInforById features.
/// </summary>
public static class GetUserInforByIdExtensionMethod
{
    public static string ToAppCode(this GetUserInforByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetUserInforById)}Feature: {statusCode}";
    }
}
