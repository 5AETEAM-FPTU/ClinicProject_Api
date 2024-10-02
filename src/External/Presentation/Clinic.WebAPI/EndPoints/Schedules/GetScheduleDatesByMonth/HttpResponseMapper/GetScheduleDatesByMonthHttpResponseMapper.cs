namespace Clinic.WebAPI.EndPoints.Schedules.GetScheduleDatesByMonth.HttpResponseMapper;

/// <summary>
///     GetSchedulesByDate extension method
/// </summary>
internal static class GetScheduleDatesByMonthHttpResponseMapper
{
    private static GetScheduleDatesByMonthHttpResponseManager _GetScheduleDatesByMonthHttpResponseManager;

    internal static GetScheduleDatesByMonthHttpResponseManager Get()
    {
        return _GetScheduleDatesByMonthHttpResponseManager ??= new();
    }
}
