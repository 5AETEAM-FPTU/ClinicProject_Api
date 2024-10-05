﻿using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Doctors.GetMedicalReportById;
using System.Linq;

namespace Clinic.MySQL.Repositories.Doctor.GetMedicalReportById;

public class GetMedicalReportByIdRepository : IGetMedicalReportByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicalReport> _reports;

    public GetMedicalReportByIdRepository(ClinicContext context)
    {
        _context = context;
        _reports = _context.Set<MedicalReport>();
    }

    public async Task<MedicalReport> GetMedicalReportByIdQueryAsync(Guid reportId, CancellationToken cancellationToken)
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
                    }
                },
                PatientInformation = new PatientInformation() 
                { 
                    Id = report.PatientInformation.Id,
                    Gender = report.PatientInformation.Gender,
                    Address = report.PatientInformation.Address,
                    DOB = report.PatientInformation.DOB,
                    FullName = report.PatientInformation.FullName,
                    PhoneNumber = report.PatientInformation.PhoneNumber
                },
                ServiceOrder = new ServiceOrder() 
                {
                    Id=report.ServiceOrderId,
                    Quantity = report.ServiceOrder.Quantity,
                    TotalPrice = report.ServiceOrder.TotalPrice,
                },
                MedicineOrder = new MedicineOrder()
                {
                    Id = report.MedicineOrderId,
                }
            })
            .FirstOrDefaultAsync();
    }
}