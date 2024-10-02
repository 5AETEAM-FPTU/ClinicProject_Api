namespace Clinic.WebAPI.EndPoints.Appointments.GetAppointmentUpcoming.HttpResponseMapper;

/// <summary>
///     GetAppointmentUpcoming extension method
/// </summary>
internal static class GetAppointmentUpcomingHttpResponseMapper
{
    private static GetAppointmentUpcomingHttpResponseManager _GetAppointmentUpcomingHttpResponseManager;

    internal static GetAppointmentUpcomingHttpResponseManager Get()
    {
        return _GetAppointmentUpcomingHttpResponseManager ??= new();
    }
}
