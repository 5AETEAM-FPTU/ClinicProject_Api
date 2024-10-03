using Clinic.Application.Features.Appointments.GetAppointmentUpcoming;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAppointmentUpcoming.Common;

internal sealed class GetAppointmentUpcomingStateBag
{
    internal GetAppointmentUpcomingRequest AppRequest { get; } = new();
}
