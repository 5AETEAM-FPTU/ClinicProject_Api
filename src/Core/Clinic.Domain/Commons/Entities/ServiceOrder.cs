using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "ServiceOrders" table.
/// </summary>
public class ServiceOrder : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public int TotalPrice { get; set; }

    public int Quantity { get; set; }

    public bool IsAllUpdated { get; set; }

    // Navigation properties.
    public MedicalReport MedicalReport { get; set; }

    // Navigation properties.
    public IEnumerable<ServiceOrderItem> ServiceOrderItems { get; set; }
}
