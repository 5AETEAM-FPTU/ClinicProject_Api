using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Users.GetAllMedicalReports;

/// <summary>
///     GetAllMedicalReport Request
/// </summary>
public class GetAllUserMedicalReportsRequest : IFeatureRequest<GetAllUserMedicalReportsResponse>
{
    [BindFrom("keyword")]
    public string Keyword { get; set; } = "";
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
