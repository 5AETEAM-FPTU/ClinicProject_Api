namespace Clinic.WebAPI.EndPoints.Notification.CreateRetreatmentNotification.HttpResponseMapper;

/// <summary>
///     CreateRetreatmentNotification extension method
/// </summary>
internal static class CreateRetreatmentNotificationHttpResponseMapper
{
    private static CreateRetreatmentNotificationHttpResponseManager _CreateRetreatmentNotificationHttpResponseManager;

    internal static CreateRetreatmentNotificationHttpResponseManager Get()
    {
        return _CreateRetreatmentNotificationHttpResponseManager ??= new();
    }
}
