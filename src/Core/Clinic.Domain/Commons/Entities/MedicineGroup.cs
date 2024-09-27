using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "MedicineGroups" table.
/// </summary>
public class MedicineGroup : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string Name { get; set; }

    public string Constant { get; set; }

    // Navigation Collections.
    public IEnumerable<Medicine> Medicines { get; set; }
}
