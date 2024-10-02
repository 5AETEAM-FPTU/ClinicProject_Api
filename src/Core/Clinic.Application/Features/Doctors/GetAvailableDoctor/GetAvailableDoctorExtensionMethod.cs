namespace Clinic.Application.Features.Doctors.GetAvailableDoctor;

/// <summary>
///     Extension Method for GetAvailableDoctor features.
/// </summary>
public static class GetAvailableDoctorExtensionMethod
{
    public static string ToAppCode(this GetAvailableDoctorResponseStatusCode statusCode)
    {
        return $"{nameof(GetAvailableDoctor)}Feature: {statusCode}";
    }
}
