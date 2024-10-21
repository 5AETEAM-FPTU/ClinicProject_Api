using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.GetUserMedicalReport;

/// <summary>
///     GetUserMedicalReport Request
/// </summary>

public class GetUserMedicalReportRequest : IFeatureRequest<GetUserMedicalReportResponse>
{
    public Guid ReportId { get; set; }
}
