using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "ServiceOrderItem" table.
/// </summary>
public class ServiceOrderItem
    : IBaseEntity,
        ICreatedEntity,
        IUpdatedEntity,
        ITemporarilyRemovedEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid ServiceOrderId { get; set; }

    public Guid ServiceId { get; set; }

    // Normal properties.
    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation properties.
    public ServiceOrder ServiceOrder { get; set; }

    public Service Service { get; set; }
}
