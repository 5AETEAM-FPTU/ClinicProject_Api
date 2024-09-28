using Clinic.Application.Features.Enums.GetAllRetreatmentType;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllRetreatmentType.Common;

/// <summary>
///     Represents the GetAllRetreatmentType state bag.
/// </summary>
internal sealed class GetAllRetreatmentTypeStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllRetreatmentTypeRequest AppRequest { get; } = new();
}
