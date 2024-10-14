using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ExaminationServices.HiddenService;

/// <summary>
///     HiddenService request validator.
/// </summary>
public sealed class HiddenServiceRequestValidator
    : FeatureRequestValidator<HiddenServiceRequest, HiddenServiceResponse>
{
    public HiddenServiceRequestValidator()
    {
    }
}
