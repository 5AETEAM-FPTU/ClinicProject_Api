using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.RemovedDoctorTemporarily;

/// <summary>
///     RemovedDoctorTemporarily Response Status Code
/// </summary>
public class RemovedDoctorTemporarilyResponse : IFeatureResponse
{
    public RemovedDoctorTemporarilyResponseStatusCode StatusCode { get; init; }
}
