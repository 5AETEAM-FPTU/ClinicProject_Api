using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class ServiceOrderItems : IBaseEntity
{
    // Primary keys.

    // Foreign keys.
    public Guid MedicalReportId { get; set; }
    public Guid ServiceId { get; set; }

    // Navigation properties.
    public MedicalReport MedicalReport { get; set; }
    public Service Service { get; set; }

    // Normal columns.


    //Normal Attribute
    public decimal PriceAtOrder { get; set; }

}
