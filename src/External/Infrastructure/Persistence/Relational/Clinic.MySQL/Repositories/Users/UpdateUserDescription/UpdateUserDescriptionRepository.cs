using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.UpdatePasswordUser;
using Clinic.Domain.Features.Repositories.Users.UpdateUserDescription;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Users.UpdateUserDescription;

internal class UpdateUserDescriptionRepository : IUpdateUserDescriptionRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public UpdateUserDescriptionRepository(ClinicContext context)
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

    public async Task<bool> UpdateUserDescriptionByIdCommandAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
