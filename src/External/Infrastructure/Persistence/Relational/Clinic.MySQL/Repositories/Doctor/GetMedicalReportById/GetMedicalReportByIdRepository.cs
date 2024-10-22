using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetMedicalReportById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetMedicalReportById;

public class GetMedicalReportByIdRepository : IGetMedicalReportByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicalReport> _reports;
    private DbSet<Feedback> _feedbacks;

    public GetMedicalReportByIdRepository(ClinicContext context)
    {
        _context = context;
        _reports = _context.Set<MedicalReport>();
        _feedbacks = _context.Set<Feedback>();
    }

    public Task<bool> IsFeedbackExistByAppointmentIdQueryAynsc(
        Guid appointmentId,
        CancellationToken cancellationToken
    )
    {
        return _feedbacks.AnyAsync(
            predicate: feedback => feedback.AppointmentId == appointmentId,
            cancellationToken: cancellationToken
        );
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
                AppointmentId = report.AppointmentId,

                Appointment = new Appointment()
                {
                    Schedule = new Schedule() { StartDate = report.Appointment.Schedule.StartDate },
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
                ServiceOrder = new ServiceOrder()
                {
                    Id = report.ServiceOrder.Id,
                    Quantity = report.ServiceOrder.Quantity,
                    TotalPrice = report.ServiceOrder.TotalPrice,
                },
                MedicineOrder = new MedicineOrder() { Id = report.MedicineOrder.Id },
            })
            .FirstOrDefaultAsync();
    }
}
