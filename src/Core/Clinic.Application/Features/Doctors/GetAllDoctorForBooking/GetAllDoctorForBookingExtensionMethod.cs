namespace Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

/// <summary>
///     Extension Method for GetAllDoctorForBooking features.
/// </summary>
public static class GetAllDoctorForBookingExtensionMethod
{
    public static string ToAppCode(this GetAllDoctorForBookingResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllDoctorForBooking)}Feature: {statusCode}";
    }
}

