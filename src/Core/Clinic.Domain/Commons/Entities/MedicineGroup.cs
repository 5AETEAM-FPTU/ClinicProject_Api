﻿using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class MedicineGroup : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string Name { get; set; }

    public string Constaint { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation Collections.
    public IEnumerable<Medicine> Medicines { get; set; }
}
