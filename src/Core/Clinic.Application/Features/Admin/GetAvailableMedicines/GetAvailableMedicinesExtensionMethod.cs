namespace Clinic.Application.Features.Admin.GetAvailableMedicines;

/// <summary>
///     Extension Method for GetAvailableMedicines features.
/// </summary>
public static class GetAvailableMedicinesExtensionMethod
{
    public static string ToAppCode(this GetAvailableMedicinesResponseStatusCode statusCode)
    {
        return $"{nameof(GetAvailableMedicines)}Feature: {statusCode}";
    }
}
