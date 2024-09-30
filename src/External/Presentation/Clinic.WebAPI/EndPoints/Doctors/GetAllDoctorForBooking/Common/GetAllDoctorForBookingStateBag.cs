using Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.Common;

internal sealed class GetAllDoctorForBookingStateBag
{
    internal GetAllDoctorForBookingRequest AppRequest { get; } = new();
}
