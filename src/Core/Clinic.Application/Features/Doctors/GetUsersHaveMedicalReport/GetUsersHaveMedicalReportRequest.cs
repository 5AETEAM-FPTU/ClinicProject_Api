using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;

/// <summary>
///     GetUsersHaveMedicalReport Request
/// </summary>
public class GetUsersHaveMedicalReportRequest : IFeatureRequest<GetUsersHaveMedicalReportResponse>
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }
}

