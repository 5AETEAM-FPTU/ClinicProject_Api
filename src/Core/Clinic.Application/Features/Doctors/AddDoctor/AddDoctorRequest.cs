using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Doctors.AddDoctor;

namespace Clinic.Application.Features.Auths.AddDoctor;

/// <summary>
///     GetProfileDoctor Request
/// </summary>

public class AddDoctorRequest : IFeatureRequest<AddDoctorResponse>
{
    public string FullName { get; init; }

    public string Email { get; init; }

    public string DoctorStaffId { get; init; }

    public string PhoneNumber { get; init; }

    public string Gender { get; init; }

    public DateTime DOB { get; init; }

    public string Address { get; init; }

    public string Specialty { get; init; }

    public string Position { get; init; }
}
