using Clinic.Application.Features.Appointments.GetUserBookedAppointment;

namespace Clinic.WebAPI.EndPoints.Appointments.GetUserBookedAppointment.Common;

internal sealed class GetUserBookedAppointmentStateBag
{
    internal GetUserBookedAppointmentRequest AppRequest { get; } = new();
}
