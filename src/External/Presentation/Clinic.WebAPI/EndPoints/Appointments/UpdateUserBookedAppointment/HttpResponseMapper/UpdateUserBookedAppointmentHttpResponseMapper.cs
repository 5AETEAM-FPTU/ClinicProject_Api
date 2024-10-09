namespace Clinic.WebAPI.EndPoints.Appointments.UpdateUserBookedAppointment.HttpResponseMapper;

internal sealed class UpdateUserBookedAppointmentHttpResponseMapper
{
    private static UpdateUserBookedAppointmentHttpResponseManager _updateUserBookedAppointmentHttpResponseManager;

    internal static UpdateUserBookedAppointmentHttpResponseManager Get()
    {
        return _updateUserBookedAppointmentHttpResponseManager ??= new();
    }
}
