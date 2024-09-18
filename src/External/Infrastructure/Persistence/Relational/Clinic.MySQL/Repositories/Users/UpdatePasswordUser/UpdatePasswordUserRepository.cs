using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.UpdatePasswordUser;
using Clinic.MySQL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Users.UpdatePasswordUser;

internal class UpdatePasswordUserRepository : IUpdatePasswordUserRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private readonly UserManager<User> _userManager;

    public UpdatePasswordUserRepository(ClinicContext context, UserManager<User> userManager)
    {
        _context = context;
        _users = _context.Set<User>();
        _userManager = userManager;
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users.AnyAsync(
            predicate: entity =>
                entity.Id == userId && entity.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            cancellationToken: cancellationToken
        );
    }
}
