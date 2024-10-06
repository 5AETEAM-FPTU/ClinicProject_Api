using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.CreateMedicalReport;

/// <summary>
///     CreateMedicalReportResponse
/// </summary>
public sealed class CreateMedicalReportResponse : IFeatureResponse
{
    public CreateMedicalReportResponseStatusCode StatusCode { get; set; }

    public BodyResponse Body { get; set; }

    public sealed class BodyResponse
    {
        public Guid MedicalReport { get; set; }
    }
}
