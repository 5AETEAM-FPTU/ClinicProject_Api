using Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;

namespace Clinic.WebAPI.EndPoints.MedicalReports.GetMedicalReportsForStaff.Common;

internal sealed class GetMedicalReportsForStaffStateBag
{
    internal GetMedicalReportsForStaffRequest AppRequest { get; } = new();
    internal static string CacheKey { get; set; }
    internal static int CacheDurationInSeconds { get; } = 60;
}
