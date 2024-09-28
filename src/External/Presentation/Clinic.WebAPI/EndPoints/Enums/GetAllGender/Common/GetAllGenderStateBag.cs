using Clinic.Application.Features.Enums.GetAllGender;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllGender.Common;

/// <summary>
///     Represents the GetAllGender state bag.
/// </summary>
internal sealed class GetAllGenderStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllGenderRequest AppRequest { get; } = new();
}