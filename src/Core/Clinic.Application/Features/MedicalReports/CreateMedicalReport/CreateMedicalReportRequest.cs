using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.CreateMedicalReport;

/// <summary>
///     CreateMedicalReportRequest
/// </summary>
public class CreateMedicalReportRequest : IFeatureRequest<CreateMedicalReportResponse>
{
    public Guid PatientId { get; set; }

    public Guid AppointmentId { get; set; }

    public string Name { get; set; }

    public string MedicalHistory { get; set; }

    public string GeneralCondition { get; set; }

    public string Weight { get; set; }

    public string Height { get; set; }

    public string Pulse { get; set; }

    public string Temperature { get; set; }

    public string BloodPresser { get; set; }

    public string Diagnosis { get; set; }
}
