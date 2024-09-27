using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "RetreatmentNotifications" table.
/// </summary>
public class RetreatmentNotification : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public DateTime ExaminationDate { get; set; }

    // Foreign key.
    public Guid PatientId { get; set; }

    public Guid RetreatmentTypeId { get; set; }

    // Navigation properties.
    public Patient Patient { get; set; }

    public RetreatmentType RetreatmentType { get; set; }
}
