using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetRecentBookedAppointments;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Doctor.GetRecentBookedAppointments;

public class GetRecentBookedAppointmentsRepository : IGetRecentBookedAppointmentsRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetRecentBookedAppointmentsRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Doctor) // Include the related Doctor entity
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }


    public async Task<IEnumerable<Appointment>> GetRecentBookedAppointmentsByDoctorIdQueryAsync(Guid userId,int size, CancellationToken cancellationToken)
    {
        return await _appointments
            .Include(appointment => appointment.Schedule)
            .Include(appointment => appointment.Patient)
            .ThenInclude(patient => patient.User)
            .Where(appointment => appointment.Schedule.DoctorId == userId)
            .OrderByDescending(appointment => appointment.CreatedAt)
            .Take(size)
            .ToListAsync(cancellationToken);       
    }


}
