namespace Clinic.Application.Features.Users.SendFeedBack;

public static class SendFeedBackExtensionMethod
{
    public static string ToAppCode(this SendFeedBackResponseStatusCode statusCode)
    {
        return $"{nameof(SendFeedBack)}Feature: {statusCode}";
    }
}
