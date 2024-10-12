using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.GetAbsentAppointment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Appointments.GetAbsentAppointment;

/// <summary>
///     Implementation of <see cref="IGetAbsentAppointmentRepository" />
/// </summary>
internal class GetAbsentAppointmentRepository : IGetAbsentAppointmentRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetAbsentAppointmentRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<IEnumerable<Appointment>> GetAbsentAppointmentByUserIdQueryAsync(
        Guid doctorId,
        CancellationToken cancellationToken
    )
    {
        return await _appointments
            .AsNoTracking()
            .Where(appointment =>
                appointment.Schedule.DoctorId == doctorId
                && appointment.AppointmentStatus.Constant.Equals("No-Show")
            )
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
                AppointmentStatus = new AppointmentStatus()
                {
                    Id = appointment.AppointmentStatus.Id,
                    Constant = appointment.AppointmentStatus.Constant,
                    StatusName = appointment.AppointmentStatus.StatusName,
                },
                Description = appointment.Description,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
