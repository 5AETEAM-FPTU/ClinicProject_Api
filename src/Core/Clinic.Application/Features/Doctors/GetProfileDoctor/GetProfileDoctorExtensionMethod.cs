namespace Clinic.Application.Features.Doctors.GetProfileDoctor;

/// <summary>
///     Extension Method for GetProfileDoctor features.
/// </summary>
public static class GetProfileDoctorExtensionMethod
{
    public static string ToAppCode(this GetProfileDoctorResponseStatusCode statusCode)
    {
        return $"{nameof(GetProfileDoctor)}Feature: {statusCode}";
    }
}
