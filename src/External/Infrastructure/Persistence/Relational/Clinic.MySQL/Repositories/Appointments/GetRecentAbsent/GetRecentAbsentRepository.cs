using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.GetRecentAbsent;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Appointments.GetRecentAbsent;

/// <summary>
///     Implementation of <see cref="IGetRecentAbsentRepository" />
/// </summary>
internal class GetRecentAbsentRepository : IGetRecentAbsentRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetRecentAbsentRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<IEnumerable<Appointment>> FindRecentAbsentQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _appointments
            .AsNoTracking()
            .Where(appointment => appointment.AppointmentStatus.Constant.Equals("No-Show"))
            .OrderByDescending(appointment => appointment.Schedule.StartDate)
            .Select(appointment => new Appointment()
            {
                Id = appointment.Id,
                Schedule = new Schedule()
                {
                    Id = appointment.Schedule.Id,
                    StartDate = appointment.Schedule.StartDate,
                    EndDate = appointment.Schedule.EndDate,
                },
                Patient = new Patient()
                {
                    UserId = appointment.Patient.UserId,
                    User = new User()
                    {
                        FullName = appointment.Patient.User.FullName,
                        Avatar = appointment.Patient.User.Avatar,
                        Gender = appointment.Patient.User.Gender,
                        PhoneNumber = appointment.Patient.User.PhoneNumber,
                    },
                    DOB = appointment.Patient.DOB,
                },
            })
            .Take(3)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
