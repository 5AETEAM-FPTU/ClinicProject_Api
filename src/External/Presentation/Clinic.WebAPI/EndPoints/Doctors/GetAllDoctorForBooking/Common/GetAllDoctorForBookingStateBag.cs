using Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.Common;

internal sealed class GetAllDoctorForBookingStateBag
{
    internal GetAllDoctorForBookingRequest AppRequest { get; } = new();
    internal static string CacheKey { get; set; }
    internal static int CacheDurationInSeconds { get; } = 60;
}
