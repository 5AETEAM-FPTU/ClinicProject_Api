using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Medicines" table.
/// </summary>
public class Medicine : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string Name { get; set; }

    public string Ingredient { get; set; }

    public string Manufacture { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign keys.
    public Guid MedicineTypeId { get; set; }

    public Guid MedicineGroupId { get; set; }

    // Navigation properties.
    public MedicineType MedicineType { get; set; }

    public MedicineGroup MedicineGroup { get; set; }

    // Navigation Collections.
    public IEnumerable<MedicineOrderItem> MedicineOrderItems { get; set; }
}
