using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ExaminationServices.UpdateService;

/// <summary>
///     UpdateService Validator
/// </summary>
public sealed class UpdateServiceRequestValidator
    : FeatureRequestValidator<UpdateServiceRequest, UpdateServiceResponse>
{
    public UpdateServiceRequestValidator()
    {

    }
}

