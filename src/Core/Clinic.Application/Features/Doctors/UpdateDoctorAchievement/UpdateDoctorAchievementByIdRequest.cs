using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdateDoctorAchievement;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateDoctorAchievementByIdRequest : IFeatureRequest<UpdateDoctorAchievementByIdResponse>
{
    public string Achievement { get; set; }

}
