using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "QueueRoom" table.
/// </summary>
public class QueueRoom : IBaseEntity, ICreatedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string Message { get; set; }

    public bool IsSuported { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    // Foreign keys.
    public Guid PatientId { get; set; }

    // Navigation properties.
    public Patient Patient { get; set; }
}
