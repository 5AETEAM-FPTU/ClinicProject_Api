namespace Clinic.Application.Features.Appointments.GetAppointmentUpcoming;

/// <summary>
///     GetAppointmentUpcoming Response Status Code
/// </summary>
public enum GetAppointmentUpcomingResponseStatusCode
{
    APPOINTMENTS_IS_NOT_FOUND,
    OPERATION_SUCCESS,
    ROLE_IS_NOT_USER,
    APPOINTMENT_DATE_NOT_FOUND
}
