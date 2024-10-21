namespace Clinic.Application.Features.Doctors.GetAllDoctorForStaff;

/// <summary>
///     Extension Method for GetAllDoctorForStaff features.
/// </summary>
public static class GetAllDoctorForStaffExtensionMethod
{
    public static string ToAppCode(this GetAllDoctorForStaffResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllDoctorForStaff)}Feature: {statusCode}";
    }
}

