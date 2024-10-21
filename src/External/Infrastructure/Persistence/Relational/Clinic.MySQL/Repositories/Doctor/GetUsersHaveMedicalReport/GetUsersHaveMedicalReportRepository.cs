using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetUsersHaveMedicalReport;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetUsersHaveMedicalReport;

internal class GetUsersHaveMedicalReportRepository : IGetUsersHaveMedicalReportRepository
{
    private readonly ClinicContext _context;
    private DbSet<Patient> _patient;

    public GetUsersHaveMedicalReportRepository(ClinicContext context)
    {
        _context = context;
        _patient = _context.Set<Patient>();
    }

    public async Task<int> CountAllUserQueryAsync(CancellationToken cancellationToken)
    {
        return await _patient
            .Where(patient =>
                patient.Appointments.Any(appointment => appointment.MedicalReport != null)
            )
            .CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Patient>> FindUsersHaveMedicalReportsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _patient
            .AsNoTracking()
            .Where(patient =>
                patient.Appointments.Any(appointment => appointment.MedicalReport != null)
            )
            .Select(entity => new Patient()
            {
                UserId = entity.UserId,
                Address = entity.Address,
                DOB = entity.DOB,
                User = new User()
                {
                    Avatar = entity.User.Avatar,
                    FullName = entity.User.FullName,
                    Gender = new Gender()
                    {
                        Id = entity.User.Gender.Id,
                        Constant = entity.User.Gender.Constant,
                        Name = entity.User.Gender.Name,
                    },
                },
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
