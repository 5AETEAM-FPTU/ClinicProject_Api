namespace Clinic.Application.Features.Enums.GetAllRetreatmentType;

/// <summary>
///     Extension Method for GetAllRetreatmentType features.
/// </summary>
public static class GetAllRetreatmentTypeExtensionMethod
{
    public static string ToAppCode(this GetAllRetreatmentTypeResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllRetreatmentType)}Feature: {statusCode}";
    }
}
