using System;
using System.Collections.Generic;
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

    public string PhoneNumber { get; init; }

    public Guid GenderId { get; init; }

    public DateTime DOB { get; init; }

    public string Address { get; init; }

    public IEnumerable<Guid> SpecialtyIds { get; init; }

    public Guid PositionId { get; init; }

    public string Role { get; init; }
}
