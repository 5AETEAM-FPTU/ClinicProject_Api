﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.LoginWithGoogle;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.LoginWithGoogle;

/// <summary>
///     Implement for LoginWithGoogle Repository
/// </summary>
public class LoginWithGoogleRepository : ILoginWithGoogleRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<RefreshToken> _refreshTokens;

    public LoginWithGoogleRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public async Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _refreshTokens
                .Where(reToken => reToken.UserId.Equals(refreshToken.UserId))
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await _refreshTokens.AddAsync(
                entity: refreshToken,
                cancellationToken: cancellationToken
            );
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateUserCommandAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _users.AddAsync(entity: user, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public Task<bool> IsUserFoundByEmailQueryAsync(
        string gmail,
        CancellationToken cancellationToken
    )
    {
        return _context.Users.AnyAsync(
            entity => entity.NormalizedEmail == gmail.ToUpper(),
            cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        string gmail,
        CancellationToken cancellationToken
    )
    {
        return _context.Users.AnyAsync(
            entity =>
                entity.Email == gmail
                && entity.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            cancellationToken
        );
    }
}