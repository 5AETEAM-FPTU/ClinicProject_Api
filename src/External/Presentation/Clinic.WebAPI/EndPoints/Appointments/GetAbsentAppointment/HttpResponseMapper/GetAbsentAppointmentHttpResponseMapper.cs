namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentAppointment.HttpResponseMapper;

/// <summary>
///     GetAbsentAppointment extension method
/// </summary>
internal static class GetAbsentAppointmentHttpResponseMapper
{
    private static GetAbsentAppointmentHttpResponseManager _GetAbsentAppointmentHttpResponseManager;

    internal static GetAbsentAppointmentHttpResponseManager Get()
    {
        return _GetAbsentAppointmentHttpResponseManager ??= new();
    }
}
