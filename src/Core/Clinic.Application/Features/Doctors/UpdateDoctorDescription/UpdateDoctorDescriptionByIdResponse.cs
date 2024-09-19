using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdateDoctorDescription;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdateDoctorDescriptionByIdResponse : IFeatureResponse
{
    public UpdateDoctorDescriptionByIdResponseStatusCode StatusCode { get; init; }

}
