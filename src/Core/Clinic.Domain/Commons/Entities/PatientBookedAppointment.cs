using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "PatientBookedAppointment" table.
/// </summary>
public class PatientBookedAppointment : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid PatientId { get; set; }

    public Guid AppointmentId { get; set; }

    // Navigation properties.
    public Patient Patient { get; set; }

    public Appointment Appointment { get; set; }
}
