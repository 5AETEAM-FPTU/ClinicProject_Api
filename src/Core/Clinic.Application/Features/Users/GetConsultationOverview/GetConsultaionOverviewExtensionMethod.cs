namespace Clinic.Application.Features.Users.GetConsultationOverview;

/// <summary>
///     Extension Method for GetConsultationOverview features.
/// </summary>
public static class GetConsultationOverviewExtensionMethod
{
    public static string ToAppCode(this GetConsultationOverviewResponseStatusCode statusCode)
    {
        return $"{nameof(GetConsultationOverview)}Feature: {statusCode}";
    }
}
