namespace Clinic.WebAPI.EndPoints.Enums.GetAllAppointmentStatus.HttpResponseMapper;

/// <summary>
///     GetAllAppointmentStatus extension method
/// </summary>
internal static class GetAllAppointmentStatusHttpResponseMapper
{
    private static GetAllAppointmentStatusHttpResponseManager _GetAllAppointmentStatusHttpResponseManager;

    internal static GetAllAppointmentStatusHttpResponseManager Get()
    {
        return _GetAllAppointmentStatusHttpResponseManager ??= new();
    }
}

