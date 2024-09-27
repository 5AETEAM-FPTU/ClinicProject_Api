using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "DoctorSpecialties" table.
/// </summary>
public class DoctorSpecialty : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid DoctorId { get; set; }

    public Guid SpecialtyID { get; set; }

    // Navigation properties.
    public Doctor Doctor { get; set; }

    public Specialty Specialty { get; set; }
}
