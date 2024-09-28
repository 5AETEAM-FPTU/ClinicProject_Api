using Clinic.Application.Features.Enums.GetAllPosition;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllPosition.Common;

/// <summary>
///     Represents the GetAllPosition state bag.
/// </summary>
internal sealed class GetAllPositionStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllPositionRequest AppRequest { get; } = new();
}
