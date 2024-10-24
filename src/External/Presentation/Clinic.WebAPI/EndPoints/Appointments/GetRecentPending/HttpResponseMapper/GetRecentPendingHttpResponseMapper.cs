namespace Clinic.WebAPI.EndPoints.Appointments.GetRecentPending.HttpResponseMapper;

/// <summary>
///     GetRecentPending extension method
/// </summary>
internal static class GetRecentPendingHttpResponseMapper
{
    private static GetRecentPendingHttpResponseManager _GetRecentPendingHttpResponseManager;

    internal static GetRecentPendingHttpResponseManager Get()
    {
        return _GetRecentPendingHttpResponseManager ??= new();
    }
}
