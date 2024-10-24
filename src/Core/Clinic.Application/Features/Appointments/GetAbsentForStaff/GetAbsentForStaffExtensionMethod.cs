namespace Clinic.Application.Features.Appointments.GetAbsentForStaff;

/// <summary>
///     Extension Method for GetAbsentForStaff features.
/// </summary>
public static class GetAbsentForStaffExtensionMethod
{
    public static string ToAppCode(this GetAbsentForStaffResponseStatusCode statusCode)
    {
        return $"{nameof(GetAbsentForStaff)}Feature: {statusCode}";
    }
}
