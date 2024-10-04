using Clinic.Application.Features.Users.GetConsultationOverview;

namespace Clinic.WebAPI.EndPoints.Users.GetConsultationOverview.Common;

internal sealed class GetConsultationOverviewStateBag
{
    internal GetConsultationOverviewRequest AppRequest { get; } = new();
    internal static string CacheKey { get; set; }
    internal static int CacheDurationInSeconds { get; } = 60;
}
