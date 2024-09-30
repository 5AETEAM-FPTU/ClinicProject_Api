namespace Clinic.WebAPI.EndPoints.Appointments.GetUserBookedAppointment.HttpResponseMapper;

/// <summary>
///     GetUserBookedAppointment extension method
/// </summary>
internal static class GetUserBookedAppointmentHttpResponseMapper
{
    private static GetUserBookedAppointmentHttpResponseManager _GetUserBookedAppointmentHttpResponseManager;

    internal static GetUserBookedAppointmentHttpResponseManager Get()
    {
        return _GetUserBookedAppointmentHttpResponseManager ??= new();
    }
}
