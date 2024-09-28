
namespace Clinic.Application.Features.Enums.GetAllAppointmentStatus;

/// <summary>
///     Extension Method for GetAllAppointmentStatus features.
/// </summary>
public static class GetAllAppointmentStatusExtensionMethod
{
    public static string ToAppCode(this GetAllAppointmentStatusResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllAppointmentStatus)}Feature: {statusCode}";
    }
}