using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "MedicineOrders" table.
/// </summary>
public class MedicineOrder : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public int TotalItem { get; set; }
    public string Note { get; set; }

    // Navigation properties.
    public MedicalReport MedicalReport { get; set; }

    // Navigation properties.
    public IEnumerable<MedicineOrderItem> MedicineOrderItems { get; set; }
}
