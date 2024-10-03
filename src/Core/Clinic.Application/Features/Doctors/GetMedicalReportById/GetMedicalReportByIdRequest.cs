using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.Doctors.GetMedicalReportById;

/// <summary>
///     GetMedicalReportById Request
/// </summary>

public class GetMedicalReportByIdRequest : IFeatureRequest<GetMedicalReportByIdResponse>
{
  public Guid ReportId { get; set; }
}
