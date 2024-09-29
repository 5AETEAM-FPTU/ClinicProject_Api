using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdatePrivateDoctorInfoByIdRequest
    : IFeatureRequest<UpdatePrivateDoctorInfoByIdResponse>
{
    public string FullName { get; set; }
    public Guid GenderId { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public Guid PositionId { get; set; }
    public IEnumerable<Guid> SpecialtiesId { get; set; }
}
