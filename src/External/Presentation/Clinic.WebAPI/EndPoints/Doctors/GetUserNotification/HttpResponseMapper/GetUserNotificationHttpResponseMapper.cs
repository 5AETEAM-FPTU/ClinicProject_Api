namespace Clinic.WebAPI.EndPoints.Doctors.GetUserNotification.HttpResponseMapper;

internal static class GetUserNotificationHttpResponseMapper
{
    private static GetUserNotificationHttpResponseManager _getUserNotificationHttpResponseManager;

    internal static GetUserNotificationHttpResponseManager Get()
    {
        return _getUserNotificationHttpResponseManager ??= new();
    }
}
