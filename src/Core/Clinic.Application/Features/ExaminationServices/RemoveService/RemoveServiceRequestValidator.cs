using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ExaminationServices.RemoveService;

/// <summary>
///     RemoveService request validator.
/// </summary>
public sealed class RemoveServiceRequestValidator
    : FeatureRequestValidator<RemoveServiceRequest, RemoveServiceResponse>
{
    public RemoveServiceRequestValidator()
    {
    }
}
