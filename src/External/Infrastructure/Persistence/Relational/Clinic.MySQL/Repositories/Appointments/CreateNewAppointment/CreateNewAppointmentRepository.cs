using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Appointments.CreateNewAppointment;

internal class CreateNewAppointmentRepository : ICreateNewAppointmentRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<AppointmentStatus> _appointmentStatus;
    private readonly DbSet<Schedule> _schedules;
    private readonly DbSet<Appointment> _appointments;

    public CreateNewAppointmentRepository(ClinicContext context)
    {
        _context = context;
        _appointmentStatus = _context.Set<AppointmentStatus>();
        _schedules = _context.Set<Schedule>();
        _appointments = _context.Set<Appointment>();
    }

    public async Task<bool> CreateNewAppointment(Appointment appointment, CancellationToken cancellationToken = default)
    {
        try {
            _appointments.Add(appointment);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        } catch (Exception ex) {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }

    public Task<AppointmentStatus> GetPendingStatusAsync(
        CancellationToken cancellationToken = default
    )
    {
        return _appointmentStatus.FirstOrDefaultAsync(
            status => status.Constant == "Pending",
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsExistSchedule(
        Guid schedueleId,
        CancellationToken cancellationToken = default
    )
    {
        return _schedules.AnyAsync(
            schedule => schedule.Id == schedueleId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsExistScheduleHadAppointment(
        Guid schedueleId,
        CancellationToken cancellationToken = default
    )
    {
        return _appointments.AnyAsync(
            appointment => appointment.ScheduleId == schedueleId,
            cancellationToken: cancellationToken
        );
    }
}
