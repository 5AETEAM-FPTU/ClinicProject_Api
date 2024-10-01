namespace Clinic.WebAPI.EndPoints.Appointments.CreateNewAppointment.HttpResponseMapper;

/// <summary>
/// Create New Appointment Http Response Mapper
/// </summary>

internal static class CreateNewAppointmentHttpResponseMapper
{
    private static CreateNewAppointmentHttpResponseManager _CreateNewAppointmentHttpResponseManager;

    internal static CreateNewAppointmentHttpResponseManager Get()
    {
        return _CreateNewAppointmentHttpResponseManager ??= new();
    }
}
