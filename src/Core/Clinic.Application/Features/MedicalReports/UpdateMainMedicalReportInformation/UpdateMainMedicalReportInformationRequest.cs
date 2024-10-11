using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;

public sealed class UpdateMainMedicalReportInformationRequest
    : IFeatureRequest<UpdateMainMedicalReportInformationResponse>
{
    public Guid ReportId { get; set; }
    public string MedicalHistory { get; set; }
    public string GeneralCondition { get; set; }
    public string Weight { get; set; }
    public string Height { get; set; }
    public string Pulse { get; set; }
    public string Temperature { get; set; }
    public string BloodPresser { get; set; }
    public string Diagnosis { get; set; }
}
