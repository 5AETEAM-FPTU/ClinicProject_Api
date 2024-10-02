using Clinic.Application.Features.Doctors.GetAllMedicalReport;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport.Common;

internal sealed class GetAllMedicalReportStateBag
{
    internal GetAllMedicalReportRequest AppRequest { get; } = new();
    internal static string CacheKey { get; set; }
    internal static int CacheDurationInSeconds { get; } = 60;
}
