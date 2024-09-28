using Clinic.Application.Features.Enums.GetAllSpecialty;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllSpecialty.Common;

/// <summary>
///     Represents the GetAllSpecialty state bag.
/// </summary>
internal sealed class GetAllSpecialtyStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllSpecialtyRequest AppRequest { get; } = new();
}