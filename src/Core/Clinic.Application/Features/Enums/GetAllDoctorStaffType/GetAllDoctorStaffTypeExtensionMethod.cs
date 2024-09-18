namespace Clinic.Application.Features.Enums.GetAllDoctorStaffType;

/// <summary>
///     Extension Method for GetAllDoctorStaffType features.
/// </summary>
public static class GetAllDoctorStaffTypeExtensionMethod
{
    public static string ToAppCode(this GetAllDoctorStaffTypeResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllDoctorStaffType)}Feature: {statusCode}";
    }
}
