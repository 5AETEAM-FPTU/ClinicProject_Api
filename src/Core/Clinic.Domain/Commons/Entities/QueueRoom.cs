using Clinic.Domain.Commons.Entities.Base;
using System;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "QueueRoom" table.
/// </summary>
public class QueueRoom : IBaseEntity, ICreatedEntity, ITemporarilyRemovedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Foreign keys.
    public Guid PatientId { get; set; }

    // Normal columns.
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }


    // Navigation properties.
    public Patient Patient { get; set; }
    public ChatRoom ChatRoom { get; set; }
}
