namespace Clinic.WebAPI.EndPoints.Schedules.UpdateSchedule.HttpResponseMapper;

/// <summary>
///     UpdateSchedule extension method
/// </summary>
internal static class UpdateScheduleHttpResponseMapper
{
    private static UpdateScheduleHttpResponseManager _UpdateScheduleHttpResponseManager;

    internal static UpdateScheduleHttpResponseManager Get()
    {
        return _UpdateScheduleHttpResponseManager ??= new();
    }
}
