using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "AppointmentStatus" table.
/// </summary>
public class AppointmentStatus : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string StatusName { get; set; }

    public string Constant { get; set; }

    // Navigation properties.
    public IEnumerable<Appointment> Appointment { get; set; }
}
