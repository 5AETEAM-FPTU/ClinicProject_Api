namespace Clinic.WebAPI.EndPoints.Schedules.GetSchedulesByDate.HttpResponseMapper;

/// <summary>
///     GetSchedulesByDate extension method
/// </summary>
internal static class GetSchedulesByDateHttpResponseMapper
{
    private static GetSchedulesByDateHttpResponseManager _GetSchedulesByDateHttpResponseManager;

    internal static GetSchedulesByDateHttpResponseManager Get()
    {
        return _GetSchedulesByDateHttpResponseManager ??= new();
    }
}
