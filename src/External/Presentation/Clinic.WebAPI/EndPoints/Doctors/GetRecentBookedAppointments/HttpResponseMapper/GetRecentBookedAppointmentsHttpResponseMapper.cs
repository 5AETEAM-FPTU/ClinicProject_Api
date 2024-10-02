namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentBookedAppointments.HttpResponseMapper;

/// <summary>
///     GetSchedulesByDate extension method
/// </summary>
internal static class GetRecentBookedAppointmentsHttpResponseMapper
{
    private static GetRecentBookedAppointmentsHttpResponseManager _getRecentBookedAppointmentsHttpResponseManager;

    internal static GetRecentBookedAppointmentsHttpResponseManager Get()
    {
        return _getRecentBookedAppointmentsHttpResponseManager ??= new();
    }
}
