using Clinic.Domain.Commons.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
