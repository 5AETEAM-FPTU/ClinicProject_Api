using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Appointments.GetUserBookedAppointment;
using Clinic.Domain.Commons.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Clinic.MySQL.Repositories.Appointments.GetUserBookedAppointment;

internal class GetUserBookedAppointmentRepository : IGetUserBookedAppointmentRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetUserBookedAppointmentRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<IEnumerable<Appointment>> GetUserBookedAppointmentByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _appointments
            .AsNoTracking()
            .Where(appointment => appointment.Patient.UserId == userId && appointment.AppointmentStatus.Constant.Equals("Pending"))
            .Select( appointment => new Appointment()
            {
                Id = appointment.Id,
                Schedule = new Schedule()
                {
                    Id = appointment.Schedule.Id,
                    StartDate = appointment.Schedule.StartDate,
                    EndDate = appointment.Schedule.EndDate,
                    Doctor = new Domain.Commons.Entities.Doctor() 
                    { 
                        UserId = appointment.Schedule.Doctor.UserId,
                        User = new User()
                        {
                            FullName = appointment.Schedule.Doctor.User.FullName,
                            Avatar = appointment.Schedule.Doctor.User.Avatar
                        }
                    }
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
