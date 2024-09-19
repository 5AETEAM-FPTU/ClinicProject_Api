namespace Clinic.Application.Commons.Abstractions.UpdateDoctorDescription;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateDoctorDescriptionByIdRequest : IFeatureRequest<UpdateDoctorDescriptionByIdResponse> 
{
    public string Description { get; set; }

}
