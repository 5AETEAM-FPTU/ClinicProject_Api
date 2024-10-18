using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetRecentMedicalReportByUserId;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetRecentMedicalReportByUserId;

internal class GetRecentMedicalReportByUserIdRepository : IGetRecentMedicalReportByUserIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicalReport> _reports;
    private DbSet<User> _users;

    public GetRecentMedicalReportByUserIdRepository(ClinicContext context)
    {
        _context = context;
        _reports = _context.Set<MedicalReport>();
        _users = _context.Set<User>();
    }

    public Task<int> CountAllMedicalReportByUserIdQueryAsync(CancellationToken cancellationToken)
    {
        return _reports.CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<MedicalReport>> FindAllMedicalReportByUserIdQueryAsync(
        int pageIndex,
        int pageSize,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _reports
            .Where(report => report.Appointment.PatientId == userId)
            .Select(report => new MedicalReport()
            {
                Id = report.Id,
                Diagnosis = report.Diagnosis,
                Name = report.Name,
                Appointment = new Appointment()
                {
                    Schedule = new Schedule()
                    {
                        StartDate = report.Appointment.Schedule.StartDate,
                        EndDate = report.Appointment.Schedule.EndDate,
                        Doctor = new Domain.Commons.Entities.Doctor()
                        {
                            UserId = report.Appointment.Schedule.DoctorId,
                            User = new User()
                            {
                                FullName = report.Appointment.Schedule.Doctor.User.FullName,
                                Avatar = report.Appointment.Schedule.Doctor.User.Avatar,
                            },
                        },
                    },
                },
            })
            .OrderByDescending(report => report.Appointment.Schedule.StartDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserFoundQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _users.AnyAsync(user => user.Id == userId, cancellationToken);
    }
}
