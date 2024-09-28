namespace Clinic.Application.Features.Enums.GetAllPosition;

/// <summary>
///     Extension Method for GetAllPosition features.
/// </summary>
public static class GetAllPositionExtensionMethod
{
    public static string ToAppCode(this GetAllPositionResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllPosition)}Feature: {statusCode}";
    }
}
