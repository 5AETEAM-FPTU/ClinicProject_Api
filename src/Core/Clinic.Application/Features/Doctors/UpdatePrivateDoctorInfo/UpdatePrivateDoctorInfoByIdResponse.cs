using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdatePrivateDoctorInfoByIdResponse : IFeatureResponse
{
    public UpdatePrivateDoctorInfoByIdResponseStatusCode StatusCode { get; init; }

}
