using Clinic.Domain.Features.Repositories.Users.UpdateUserPrivateInfo;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Repositories.Users.UpdateUserPrivateInfo;

internal class UpdateUserPrivateInfoRepository : IUpdateUserPrivateInfoRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public UpdateUserPrivateInfoRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Patient) // Include the related Patient entity
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<bool> UpdateUserPrivateInfoByIdCommandAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
