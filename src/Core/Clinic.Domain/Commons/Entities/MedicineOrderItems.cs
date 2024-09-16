using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class MedicineOrderItems : IBaseEntity
{
    // Primary keys.

    // Foreign keys.
    public Guid MedicalReportId { get; set; }
    public Guid MedicineId { get; set; }

    // Navigation properties.
    public MedicalReport MedicalReport { get; set; }
    public Medicine Medicine { get; set; }

    // Normal columns.


    //Normal Attribute
    public decimal PriceAtOrder { get; set; }
    public int Quantity { get; set; }
    public Guid TimeUsing { get; set; }
    public bool IsPayment { get; set; }

}
