using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdateDoctorAchievement;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdateDoctorAchievementByIdResponse : IFeatureResponse
{
    public UpdateDoctorAchievementByIdResponseStatusCode StatusCode { get; init; }

}
