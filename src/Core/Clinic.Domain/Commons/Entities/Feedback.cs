using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Feedback" entity.
/// </summary>
public class Feedback : IBaseEntity, ICreatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string Comment { get; set; }

    public int Vote { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    // Foreign key.
    public Guid DoctorId { get; set; }

    public Guid AppointmentId { get; set; }

    // Navigation properties.
    public Appointment Appointment { get; set; }
}
