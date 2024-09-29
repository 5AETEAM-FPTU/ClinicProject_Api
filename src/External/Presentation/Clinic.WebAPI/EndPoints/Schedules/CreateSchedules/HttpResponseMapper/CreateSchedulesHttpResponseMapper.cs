namespace Clinic.WebAPI.EndPoints.Schedules.CreateSchedules.HttpResponseMapper;

/// <summary>
///     CreateSchedules extension method
/// </summary>
internal static class CreateSchedulesHttpResponseMapper
{
    private static CreateSchedulesHttpResponseManager _CreateSchedulesHttpResponseManager;

    internal static CreateSchedulesHttpResponseManager Get()
    {
        return _CreateSchedulesHttpResponseManager ??= new();
    }
}
