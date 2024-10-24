namespace Clinic.WebAPI.EndPoints.Appointments.GetRecentAbsent.HttpResponseMapper;

/// <summary>
///     GetRecentAbsent extension method
/// </summary>
internal static class GetRecentAbsentHttpResponseMapper
{
    private static GetRecentAbsentHttpResponseManager _GetRecentAbsentHttpResponseManager;

    internal static GetRecentAbsentHttpResponseManager Get()
    {
        return _GetRecentAbsentHttpResponseManager ??= new();
    }
}
