using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetAllMedicalReport;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        Guid doctorId,
        CancellationToken cancellationToken
    )
    {
        return await _reports
            .Where(report => report.Appointment.Schedule.Doctor.UserId == doctorId)
            .Select(report => new MedicalReport()
            {
                Id = report.Id,
                Diagnosis = report.Diagnosis,
                PatientInformation = new PatientInformation() 
                { 
                    Id  = report.PatientInformation.Id,
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
            .ToListAsync(cancellationToken: cancellationToken);
    }
}

