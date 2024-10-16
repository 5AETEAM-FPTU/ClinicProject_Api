using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetAllMedicalReports;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Users.GetAllMedicalReports;

public class GetAllUserMedicalReportsRepository : IGetAllUserMedicalReportsRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicalReport> _medicalReports;

    public GetAllUserMedicalReportsRepository(ClinicContext context)
    {
        _context = context;
        _medicalReports = _context.Set<MedicalReport>();
    }

    public async Task<int> CountAllServicesQueryAsync(string keyword, Guid userId, CancellationToken cancellationToken)
    {
        var results = _medicalReports.AsNoTracking().AsQueryable();
        if (keyword != default)
        {
            results = results.Where(entity =>
                entity.Appointment.Schedule.Doctor.UserId == userId
                && entity.Appointment.Schedule.Doctor.User.FullName.Contains(keyword)
            );
        }

        return await results.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<MedicalReport>> FindAllMedicalReportsByUserIdQueryAsync(int pageIndex, int pageSize, string keyword, Guid userId, CancellationToken cancellationToken)
    {
        return await _medicalReports
            .AsNoTracking()
            .Where(entity =>
                entity.Appointment.Schedule.Doctor.UserId == userId
                && entity.Appointment.Schedule.Doctor.User.FullName.Contains(keyword)
            )
            .Select(entity => new MedicalReport()
            {
                Id = entity.Id,
                Diagnosis = entity.Diagnosis,
                Appointment = new ()
                {
                    ExaminationDate = entity.Appointment.ExaminationDate,
                    Schedule = new Schedule()
                    {
                        Doctor = new()
                        {
                            User = new User()
                            {
                                Id = entity.Appointment.Schedule.Doctor.User.Id,
                                FullName = entity.Appointment.Schedule.Doctor.User.FullName,
                                Avatar = entity.Appointment.Schedule.Doctor.User.Avatar,
                            }
                        }
                    }
                }
            })
            .OrderByDescending(entity => entity.Appointment.ExaminationDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }





}
