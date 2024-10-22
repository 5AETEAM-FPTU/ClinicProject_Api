
namespace Clinic.Application.Features.Feedbacks.ViewFeedback;

public static class ViewFeedBackExtensionMethod
{
    public static string ToAppCode(this ViewFeedBackResponseStatusCode statusCode)
    {
        return $"{nameof(ViewFeedback)}Feature: {statusCode}";
    }
}
