using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.RemoveMedicineTemporarily;

/// <summary>
///     RemoveMedicineTemporarily Response Status Code
/// </summary>
public class RemoveMedicineTemporarilyResponse : IFeatureResponse
{
    public RemoveMedicineTemporarilyResponseStatusCode StatusCode { get; init; }
}
