

using Clinic.Application.Features.Users.GetAllDoctor;

namespace Clinic.WebAPI.EndPoints.Users.GetAllDoctor.Common;

/// <summary>
///     Represents the GetAllDoctors state bag.
/// </summary>
internal sealed class GetAllDoctorStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllDoctorRequest AppRequest { get; } = new();
}
