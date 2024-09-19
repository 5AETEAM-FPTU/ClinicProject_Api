using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Auths.AddDoctor;

namespace Clinic.Application.Features.Doctors.AddDoctor;

/// <summary>
///     AddDoctor request validator.
/// </summary>
public sealed class AddDoctorRequestValidator
    : FeatureRequestValidator<AddDoctorRequest, AddDoctorResponse>
{
    public AddDoctorRequestValidator() { }
}
