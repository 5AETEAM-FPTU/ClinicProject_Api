using Clinic.Domain.Features.Appointments.UpdateAppointmentStatus;
using Clinic.MySQL.Data.Context;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;
internal class UpdateAppointmentStatusRepository : IUpdateAppointmentStatusRepository {
    private readonly ClinicContext _context;

    public UpdateAppointmentStatusRepository(ClinicContext context) {
        _context = context;
    }
}