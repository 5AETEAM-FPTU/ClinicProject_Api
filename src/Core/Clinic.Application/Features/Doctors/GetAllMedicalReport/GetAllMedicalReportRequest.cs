using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Doctors.GetAllMedicalReport;

/// <summary>
///     GetAllMedicalReport Request
/// </summary>
public class GetAllMedicalReportRequest : IFeatureRequest<GetAllMedicalReportResponse>
{
    [BindFrom("keyword")]
    public string? Keyword { get; set; }

    [BindFrom("lastReportDate")]
    public DateTime? LastReportDate { get; set; }

    public int PageSize { get; set; } = 2;
}
