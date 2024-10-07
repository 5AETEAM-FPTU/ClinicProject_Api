using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicalReports.CreateMedicalReport;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.MedicalReports.CreateMedicalReport;

/// <summary>
///    Implementation of ICreateMedicalReportRepository
/// </summary>
internal class CreateMedicalReportRepository : ICreateMedicalReportRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<User> _users;
    private readonly DbSet<MedicalReport> _medicalReports;

    public CreateMedicalReportRepository(ClinicContext context)
    {
        _context = context;
        _appointments = context.Set<Appointment>();
        _users = context.Set<User>();
        _medicalReports = context.Set<MedicalReport>();
    }

    public async Task<bool> CreateMedicalReportCommandAsync(
        MedicalReport newMedicalReport,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _medicalReports.Add(entity: newMedicalReport);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            return false;
        }
    }

    public Task<User> FindPatientByIdQueryAsync(Guid patientId, CancellationToken cancellationToken)
    {
        return _users
            .Where(predicate: entity => entity.Id == patientId)
            .Select(selector: entity => new User()
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Avatar = entity.Avatar,
                PhoneNumber = entity.PhoneNumber,
                Gender = new Gender()
                {
                    Name = entity.Gender.Name,
                    Constant = entity.Gender.Constant,
                },
                Patient = new() { Address = entity.Patient.Address, DOB = entity.Patient.DOB, }
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsAppointmentFoundByIdQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken
    )
    {
        return _appointments.AnyAsync(
            predicate: entity => entity.Id == appointmentId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsAppointmentReportedQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken
    )
    {
        return _appointments.AnyAsync(
            predicate: entity =>
                entity.Id == appointmentId && !Equals(entity.MedicalReport, default),
            cancellationToken: cancellationToken
        );
    }
}
