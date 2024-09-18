using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace Clinic.Application.Commons.Abstractions.UpdatePrivateDoctorInfoById;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdatePrivateDoctorInfoByIdRequest : IFeatureRequest<UpdatePrivateDoctorInfoByIdResponse> 
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public string Position { get; set; }
    public string Specialty { get; set; }

}
