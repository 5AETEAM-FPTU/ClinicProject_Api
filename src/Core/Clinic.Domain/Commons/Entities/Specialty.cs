using System;
using System.Collections;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Specialtys" enum.
/// </summary>
public class Specialty : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string Name { get; set; }

    public string Constant { get; set; }

    public IEnumerable<DoctorSpecialty> DoctorSpecialties { get; set; }
}
