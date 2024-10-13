using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ExaminationServices.RemoveService;

/// <summary>
///     RemoveService Response Status Code
/// </summary>
public class RemoveServiceResponse : IFeatureResponse
{
    public RemoveServiceResponseStatusCode StatusCode { get; init; }
}
