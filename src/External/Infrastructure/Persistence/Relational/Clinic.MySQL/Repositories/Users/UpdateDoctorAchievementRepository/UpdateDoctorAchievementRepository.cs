using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.UpdateProfileDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Users.UpdateDoctorAchievementRepository;

internal class UpdateDoctorAchievementRepository : IUpdateDoctorAchievementRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public UpdateDoctorAchievementRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public async Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Doctor) // Include the related Doctor entity
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<bool> UpdateDoctorAchievementByIdCommandAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
