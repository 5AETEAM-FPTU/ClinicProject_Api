using Clinic.Application.Features.Users.UpdateDoctorDescription;

namespace Clinic.Application.Commons.Abstractions.UpdateDoctorDescription;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdateDoctorDescriptionByIdResponse : IFeatureResponse
{
    public UpdateDoctorDescriptionByIdResponseStatusCode StatusCode { get; init; }

}
