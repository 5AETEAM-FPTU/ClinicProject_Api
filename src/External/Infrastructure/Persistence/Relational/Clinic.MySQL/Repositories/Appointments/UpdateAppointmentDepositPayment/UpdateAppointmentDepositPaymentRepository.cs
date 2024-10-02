using Clinic.Domain.Features.Repositories.Appointments.UpdateAppointmentDepositPayment;
using Clinic.MySQL.Data.Context;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

internal sealed class UpdateAppointmentDepositPaymentRepository : IUpdateAppointmentDepositPaymentRepository
{

    private readonly ClinicContext _context;

    public UpdateAppointmentDepositPaymentRepository(ClinicContext context)
    {
        _context = context;
    }
}