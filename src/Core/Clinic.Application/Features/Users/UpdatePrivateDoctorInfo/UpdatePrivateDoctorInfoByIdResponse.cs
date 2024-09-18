
using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;
using System;

namespace Clinic.Application.Commons.Abstractions.UpdatePrivateDoctorInfoById;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdatePrivateDoctorInfoByIdResponse : IFeatureResponse
{
    public UpdatePrivateDoctorInfoByIdResponseStatusCode StatusCode { get; init; }

}
