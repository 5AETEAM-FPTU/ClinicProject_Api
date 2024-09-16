using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class Medicine : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Foreign keys.
    public Guid MedicineTypeId { get; set; }
    public Guid MedicineGroupId { get; set; }

    // Navigation properties.
    public MedicineType MedicineType { get; set; }
    public MedicineGroup MedicineGroup { get; set; }
    public IEnumerable<MedicineOrderItems>  MedicineOrderItems { get; set; }

    // Normal columns.
    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    //Normal Attribute
    public string Name { get; set; }
    public string Ingredient { get; set; }
    public string Manufacture { get; set; }
    public decimal ImmigrationCost { get; set; }
    public decimal SellingCost { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string Indication { get; set; }
    public string Dose { get; set; }

}
