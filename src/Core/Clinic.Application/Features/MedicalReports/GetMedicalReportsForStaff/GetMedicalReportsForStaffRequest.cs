using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     GetMedicalReportsForStaff Request
/// </summary>
public class GetMedicalReportsForStaffRequest : IFeatureRequest<GetMedicalReportsForStaffResponse>
{
    [BindFrom("keyword")]
    public string? Keyword { get; set; }

    [BindFrom("lastReportDate")]
    public DateTime? LastReportDate { get; set; }
}
