using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;

namespace Clinic.Application.Commons.Abstractions.UpdatePrivateDoctorInfoById;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdatePrivateDoctorInfoByIdResponse : IFeatureResponse
{
    public UpdatePrivateDoctorInfoByIdResponseStatusCode StatusCode { get; init; }

}
