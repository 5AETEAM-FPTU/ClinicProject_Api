using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Appointment" table.
/// </summary>
public class Appointment : IBaseEntity, ICreatedEntity, ITemporarilyRemovedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public bool ReExamination { get; set; }

    public DateTime ExaminationDate { get; set; }

    public bool DepositPayment { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign keys.
    public Guid PatientId { get; set; }

    public Guid StatusId { get; set; }

    public Guid ScheduleId { get; set; }

    // Navigation properties.
    public AppointmentStatus AppointmentStatus { get; set; }

    public OnlinePayment OnlinePayment { get; set; }

    public Patient Patient { get; set; }

    public Schedule Schedule { get; set; }

    public MedicalReport MedicalReport { get; set; }

    public Feedback Feedback { get; set; }
}
