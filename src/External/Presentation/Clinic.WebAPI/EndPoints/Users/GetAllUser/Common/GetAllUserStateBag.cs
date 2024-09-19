using Clinic.Application.Features.Users.GetAllUser;

namespace Clinic.WebAPI.EndPoints.Users.GetAllUser.Common;

/// <summary>
///     Represents the GetAllDoctors state bag.
/// </summary>
internal sealed class GetAllUserStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllUserRequest AppRequest { get; } = new();
}
