using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "PatientInformation" table.
/// </summary>
public class PatientInformation : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string FullName { get; set; }

    public string Gender { get; set; }

    public DateTime DOB { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    // Navigation collections.
    public MedicalReport MedicalReport { get; set; }
}
