namespace Clinic.Application.Features.Notification.CreateRetreatmentNotification;

public static class CreateRetreatmentNotificationExtensionMethod
{
    public static string ToAppCode(this CreateRetreatmentNotificationResponseStatusCode statusCode)
    {
        return $"{nameof(CreateRetreatmentNotification)}Feature: {statusCode}";
    }
}
