namespace Clinic.WebAPI.EndPoints.Schedules.RemoveAllSchedules.HttpResponseMapper;

/// <summary>
///     UpdateSchedule extension method
/// </summary>
internal static class RemoveAllSchedulesHttpResponseMapper
{
    private static RemoveAllSchedulesHttpResponseManager _removeAllSchedulesHttpResponseManager;

    internal static RemoveAllSchedulesHttpResponseManager Get()
    {
        return _removeAllSchedulesHttpResponseManager ??= new();
    }
}
