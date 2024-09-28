using Clinic.Application.Features.Enums.GetAllAppointmentStatus;
using Clinic.Application.Features.Users.GetAllDoctor;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllAppointmentStatus.Common;

/// <summary>
///     Represents the GetAllAppointmentStatus state bag.
/// </summary>
internal sealed class GetAllAppointmentStatusStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllAppointmentStatusRequest AppRequest { get; } = new();
}