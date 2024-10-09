namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

internal static class UpdateAppointmentStatusHttpReponseMapper
{
    private static UpdateAppointmentStatusHttpResponseManager _updateAppointmentStatusHttpResponseManager;

    internal static UpdateAppointmentStatusHttpResponseManager Get()
    {
        return _updateAppointmentStatusHttpResponseManager ??= new();
    }
}
