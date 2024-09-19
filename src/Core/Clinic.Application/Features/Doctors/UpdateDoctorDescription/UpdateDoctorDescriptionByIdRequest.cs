using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdateDoctorDescription;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateDoctorDescriptionByIdRequest : IFeatureRequest<UpdateDoctorDescriptionByIdResponse>
{
    public string Description { get; set; }

}
