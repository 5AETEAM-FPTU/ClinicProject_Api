using Clinic.Application.Features.Users.GetRecentMedicalReport;

namespace Clinic.WebAPI.EndPoints.Users.GetRecentMedicalReport.Common;

internal sealed class GetRecentMedicalReportStateBag
{
    internal GetRecentMedicalReportRequest AppRequest { get; } = new();
    internal static string CacheKey { get; set; }
    internal static int CacheDurationInSeconds { get; } = 60;
}