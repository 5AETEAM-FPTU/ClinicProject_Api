using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.ExaminationServices.RemoveService;

namespace Clinic.Application.Features.ExaminationServices.HiddenService;

/// <summary>
///     HiddenService Response Status Code
/// </summary>
public class HiddenServiceResponse : IFeatureResponse
{
    public HiddenServiceResponseStatusCode StatusCode { get; init; }
}
