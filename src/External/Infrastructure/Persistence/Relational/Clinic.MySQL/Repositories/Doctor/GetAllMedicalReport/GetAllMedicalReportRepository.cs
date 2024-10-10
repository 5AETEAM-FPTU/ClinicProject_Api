using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetAllMedicalReport;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetAllMedicalReport;

internal class GetAllMedicalReportRepository : IGetAllMedicalReportRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicalReport> _reports;

    public GetAllMedicalReportRepository(ClinicContext context)
    {
        _context = context;
        _reports = _context.Set<MedicalReport>();
    }

    public async Task<IEnumerable<MedicalReport>> FindAllMedicalReportByDoctorIdQueryAsync(
        string keyword,
        DateTime? lastReportDate,
        int pageSize,
        Guid doctorId,
        CancellationToken cancellationToken
    )
    {
        var latestDateBeforeGivenDate = await _reports
            .Where(report =>
                report.Appointment.Schedule.Doctor.UserId == doctorId
                && report.Appointment.Schedule.StartDate.Date
                    < (lastReportDate.HasValue ? lastReportDate.Value.Date : DateTime.Now.Date)
            )
            .OrderByDescending(report => report.Appointment.Schedule.StartDate)
            .Select(report => report.Appointment.Schedule.StartDate.Date)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var reportsQuery = _reports.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            reportsQuery = reportsQuery.Where(entity =>
                entity.PatientInformation.FullName.Contains(keyword)
            );
        }

        reportsQuery = reportsQuery.Where(report =>
            report.Appointment.Schedule.Doctor.UserId == doctorId
        );

        reportsQuery = reportsQuery.Where(report =>
            report.Appointment.Schedule.StartDate.Date == latestDateBeforeGivenDate.Date
        );

        return await reportsQuery
            .OrderByDescending(report => report.Appointment.Schedule.StartDate)
            .Select(report => new MedicalReport()
            {
                Id = report.Id,
                Diagnosis = report.Diagnosis,
                PatientInformation = new PatientInformation()
                {
                    Id = report.PatientInformation.Id,
                    FullName = report.PatientInformation.FullName,
                    DOB = report.PatientInformation.DOB,
                    Gender = report.PatientInformation.Gender,
                    PhoneNumber = report.PatientInformation.PhoneNumber,
                },
                Appointment = new Appointment()
                {
                    Schedule = new Schedule()
                    {
                        StartDate = report.Appointment.Schedule.StartDate,
                        EndDate = report.Appointment.Schedule.EndDate,
                    }
                },
            })
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
