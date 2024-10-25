using Clinic.Application.Features.Feedbacks.ViewFeedback;

namespace Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;

public static class DoctorGetAllFeedBackExtensionMethod
{
    public static string ToAppCode(this DoctorGetAllFeedBackResponseStatusCode statusCode)
    {
        return $"{nameof(DoctorGetAllFeedbacks)}Feature: {statusCode}";
    }
}
