namespace Clinic.Application.Features.Feedbacks.SendFeedBack;

public static class SendFeedBackExtensionMethod
{
    public static string ToAppCode(this SendFeedBackResponseStatusCode statusCode)
    {
        return $"{nameof(SendFeedBack)}Feature: {statusCode}";
    }
}
