using Clinic.Domain.Features.Repositories.Users.UpdateUserPrivateInfo;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Users.UpdateUserPrivateInfo;

internal class UpdateUserPrivateInfoRepository : IUpdateUserPrivateInfoRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<Gender> _genders;

    public UpdateUserPrivateInfoRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _genders = _context.Set<Gender>();
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

    public async Task<Gender> GetGenderByIdAsync(Guid? genderId, CancellationToken cancellationToken)
    {
        return await _genders.Where(gender => gender.Id == genderId)
                             .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsGenderIdExistAsync(Guid? genderId, CancellationToken cancellationToken)
    {
        return await _genders
                     .AnyAsync(gender => gender.Id == genderId, cancellationToken);
    }

}
