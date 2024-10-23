namespace Clinic.Application.Features.Admin.RemovedDoctorTemporarily;

/// <summary>
///     Extension Method for RemovedDoctorTemporarily features.
/// </summary>
public static class RemovedDoctorTemporarilyExtensionMethod
{
    public static string ToAppCode(this RemovedDoctorTemporarilyResponseStatusCode statusCode)
    {
        return $"{nameof(RemovedDoctorTemporarily)}Feature: {statusCode}";
    }
}
