using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetAppointmentsByDate;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Doctor.GetAppointmentsByDate;

public class GetAppointmentsByDateRepository : IGetAppointmentsByDateRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetAppointmentsByDateRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDateQueryAsync(DateTime startDate, DateTime endDate, Guid userId, CancellationToken cancellationToken = default)
    {
        return await _appointments
            .Include(appointment => appointment.Patient)
            .ThenInclude(patient => patient.User)
            .ThenInclude(user => user.Gender)
            .Include(appointment => appointment.Schedule)
            .Include(appointment => appointment.AppointmentStatus)
            .Where(appointment =>
                    appointment.Schedule.StartDate >= startDate
                    && appointment.Schedule.EndDate <= endDate
                    && appointment.Schedule.DoctorId == userId
                    )
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Doctor) // Include the related Doctor entity
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
}
