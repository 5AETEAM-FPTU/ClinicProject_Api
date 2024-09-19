using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

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
