namespace Clinic.Application.Features.Doctors.GetIdsDoctor;

/// <summary>
///     Extension Method for GetIdsDoctor features.
/// </summary>
public static class GetIdsDoctorExtensionMethod
{
    public static string ToAppCode(this GetIdsDoctorResponseStatusCode statusCode)
    {
        return $"{nameof(GetIdsDoctor)}Feature: {statusCode}";
    }
}
