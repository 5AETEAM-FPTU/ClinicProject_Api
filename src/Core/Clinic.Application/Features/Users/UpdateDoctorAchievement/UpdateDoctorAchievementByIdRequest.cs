
using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Users.UpdateDoctorAchievement;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateDoctorAchievementByIdRequest : IFeatureRequest<UpdateDoctorAchievementByIdResponse> 
{
    public string Achievement { get; set; }

}
