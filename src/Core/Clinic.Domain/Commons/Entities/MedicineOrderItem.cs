using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "MedicineOrderItems" table.
/// </summary>
public class MedicineOrderItem : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid MedicineOrderId { get; set; }

    public Guid MedicineId { get; set; }

    // Normal Properties.
    public string Description { get; set; }

    public int Quantity { get; set; }

    // Navigation properties.
    public MedicineOrder MedicineOrder { get; set; }

    public Medicine Medicine { get; set; }
}
