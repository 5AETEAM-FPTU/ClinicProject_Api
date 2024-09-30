namespace Clinic.WebAPI.EndPoints.Doctors.GetAppointmentsByDate.HttpResponseMapper;

/// <summary>
///     GetSchedulesByDate extension method
/// </summary>
internal static class GetAppointmentByDateHttpResponseMapper
{
    private static GetAppointmentByDateHttpResponseManager _getAppointmentByDateHttpResponseManager;

    internal static GetAppointmentByDateHttpResponseManager Get()
    {
        return _getAppointmentByDateHttpResponseManager ??= new();
    }
}
