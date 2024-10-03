using Clinic.Application.Features.Appointments.GetAbsentAppointment;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentAppointment.Common;

internal sealed class GetAbsentAppointmentStateBag
{
    internal GetAbsentAppointmentRequest AppRequest { get; } = new();
}
