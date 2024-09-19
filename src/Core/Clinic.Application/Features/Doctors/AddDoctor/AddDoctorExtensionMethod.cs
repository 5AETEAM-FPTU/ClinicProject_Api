namespace Clinic.Application.Features.Doctors.AddDoctor;

/// <summary>
///     Extension Method for AddDoctor features.
/// </summary>
public static class AddDoctorExtensionMethod
{
    public static string ToAppCode(this AddDoctorResponseStatusCode statusCode)
    {
        return $"{nameof(AddDoctor)}Feature: {statusCode}";
    }
}
