using System;
using System.Collections.Generic;
using System.ComponentModel;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "MedicalReports" table.
/// </summary>
public class MedicalReport : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string Name { get; set; }

    public string MedicalHistory { get; set; }

    public int TotalPrice { get; set; }

    public string GeneralCondition { get; set; }

    public string Weight { get; set; }

    public string Height { get; set; }

    public string Pulse { get; set; }

    public string Temperature { get; set; }

    public string BloodPresser { get; set; }

    public string Diagnosis { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign keys.
    public Guid ServiceOrderId { get; set; }

    public Guid MedicineOrderId { get; set; }

    public Guid PatientInformationId { get; set; }

    public Guid AppointmentId { get; set; }

    // Navigation properties.
    public ServiceOrder ServiceOrder { get; set; }

    public MedicineOrder MedicineOrder { get; set; }

    public Appointment Appointment { get; set; }

    public PatientInformation PatientInformation { get; set; }
}
