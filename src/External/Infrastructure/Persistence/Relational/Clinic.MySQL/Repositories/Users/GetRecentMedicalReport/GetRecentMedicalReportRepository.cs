using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Users.GetRecentMedicalReport;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Users.GetRecentMedicalReport;

internal class GetRecentMedicalReportRepository : IGetRecentMedicalReportRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicalReport> _reports;

    public GetRecentMedicalReportRepository(ClinicContext context)
    {
        _context = context;
        _reports = _context.Set<MedicalReport>();
    }

    public async Task<IEnumerable<MedicalReport>> FindAllMedicalReportByUserIdQueryAsync(
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
                        Doctor =  new Domain.Commons.Entities.Doctor()
                        {
                            UserId = report.Appointment.Schedule.DoctorId,
                            User = new User()
                            {
                                FullName = report.Appointment.Schedule.Doctor.User.FullName,
                                Avatar = report.Appointment.Schedule.Doctor.User.Avatar,
                            }
                        }
                    }
                },
            })
            .OrderByDescending(report => report.Appointment.Schedule.StartDate)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
