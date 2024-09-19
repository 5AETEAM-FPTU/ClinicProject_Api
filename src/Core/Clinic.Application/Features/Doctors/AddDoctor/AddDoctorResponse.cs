using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.AddDoctor;

/// <summary>
///     AddDoctor Response Status Code
/// </summary>
public class AddDoctorResponse : IFeatureResponse
{
    public AddDoctorResponseStatusCode StatusCode { get; init; }
}
