using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.GetRecentPending;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Data.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Appointments.GetRecentPending;

/// <summary>
///     Implementation of <see cref="IGetRecentPendingRepository" />
/// </summary>
internal class GetRecentPendingRepository : IGetRecentPendingRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetRecentPendingRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<IEnumerable<Appointment>> FindRecentPendingQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _appointments
            .AsNoTracking()
            .Where(appointment =>
                appointment.AppointmentStatus.Id.Equals(EnumConstant.AppointmentStatus.PENDING)
            )
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
            .Take(2)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
