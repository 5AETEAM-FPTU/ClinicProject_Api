using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetUserMedicalReport;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Users.GetUserMedicalReport;

public class GetUserMedicalReportRepository : IGetUserMedicalReportRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicalReport> _reports;
    private DbSet<Domain.Commons.Entities.Doctor> _doctors;

    public GetUserMedicalReportRepository(ClinicContext context)
    {
        _context = context;
        _reports = _context.Set<MedicalReport>();
        _doctors = _context.Set<Domain.Commons.Entities.Doctor>();
    }

    public async Task<MedicalReport> GetMedicalReportByIdQueryAsync(
        Guid reportId,
        CancellationToken cancellationToken
    )
    {
        return await _reports
            .Where(report => report.Id == reportId)
            .Select(report => new MedicalReport()
            {
                Id = report.Id,
                Diagnosis = report.Diagnosis,
                MedicalHistory = report.MedicalHistory,
                BloodPresser = report.BloodPresser,
                Pulse = report.Pulse,
                Temperature = report.Temperature,
                Weight = report.Weight,
                Height = report.Height,
                GeneralCondition = report.GeneralCondition,
                Appointment = new Appointment()
                {
                    Schedule = new Schedule()
                    {
                        StartDate = report.Appointment.Schedule.StartDate,
                        Doctor = new Domain.Commons.Entities.Doctor()
                        {
                            UserId = report.Appointment.Schedule.Doctor.UserId,
                            Position = new Position()
                            {
                                Id = report.Appointment.Schedule.Doctor.Position.Id,
                                Constant = report.Appointment.Schedule.Doctor.Position.Constant,
                                Name = report.Appointment.Schedule.Doctor.Position.Name,
                            },
                            User = new User()
                            {
                                FullName = report.Appointment.Schedule.Doctor.User.FullName,
                                Avatar = report.Appointment.Schedule.Doctor.User.Avatar,
                            },
                            DoctorSpecialties =
                                report.Appointment.Schedule.Doctor.DoctorSpecialties.Select(
                                    specialty => new DoctorSpecialty()
                                    {
                                        Specialty = new Specialty()
                                        {
                                            Id = specialty.Specialty.Id,
                                            Constant = specialty.Specialty.Constant,
                                            Name = specialty.Specialty.Name,
                                        },
                                    }
                                ),
                        },
                    },
                    Patient = new Patient()
                    {
                        User = new User() { Avatar = report.Appointment.Patient.User.Avatar },
                    },
                },
                PatientInformation = new PatientInformation()
                {
                    Id = report.PatientInformation.Id,
                    Gender = report.PatientInformation.Gender,
                    Address = report.PatientInformation.Address,
                    DOB = report.PatientInformation.DOB,
                    FullName = report.PatientInformation.FullName,
                    PhoneNumber = report.PatientInformation.PhoneNumber,
                },
                MedicineOrderId = report.MedicineOrderId,
                ServiceOrderId = report.ServiceOrderId,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsAppointmentHasFeedbackQueryAsync(
        Guid reportId,
        CancellationToken cancellationToken
    )
    {
        return await _reports
            .Where(report => report.Id == reportId)
            .AnyAsync(report => report.Appointment.Feedback != null);
    }
}
