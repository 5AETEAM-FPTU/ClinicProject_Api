namespace Clinic.WebAPI.EndPoints.Schedules.RemoveSchedule.HttpResponseMapper;

/// <summary>
///     UpdateSchedule extension method
/// </summary>
internal static class RemoveScheduleHttpResponseMapper
{
    private static RemoveScheduleHttpResponseManager _removeScheduleHttpResponseManager;

    internal static RemoveScheduleHttpResponseManager Get()
    {
        return _removeScheduleHttpResponseManager ??= new();
    }
}
