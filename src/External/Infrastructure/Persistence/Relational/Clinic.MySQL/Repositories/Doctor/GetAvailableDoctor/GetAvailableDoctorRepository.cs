using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetAvailableDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetAvailableDoctor;

internal class GetAvailableDoctorRepository : IGetAvailableDoctorRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _doctors;

    public GetAvailableDoctorRepository(ClinicContext context)
    {
        _context = context;
        _doctors = _context.Set<User>();
    }

    public async Task<IEnumerable<User>> GetAvailableDoctorQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _doctors
            .AsNoTracking()
            .Where(entity => entity.Doctor.IsOnDuty)
            .Select(entity => new User()
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Avatar = entity.Avatar,
            })
            .ToListAsync(cancellationToken);
    }
}
