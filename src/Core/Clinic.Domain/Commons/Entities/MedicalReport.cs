using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class MedicalReport : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Foreign keys.
    // Navigation properties.
    public IEnumerable<ServiceOrderItems> ServiceOrderItems { get; set; }
    public IEnumerable<MedicineOrderItems> MedicineOrderItems { get; set; }

    // Normal columns.
    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    //Normal Attribute
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal TotalPrice { get; set; }

}
