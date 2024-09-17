using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.ForgotPassword;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.ForgotPassword;

/// <summary>
///     Implement for ForgotPassword Repository
/// </summary>
public class ForgotPasswordRepository : IForgotPasswordRepository
{
    private readonly ClinicContext _context;
    private DbSet<UserToken> _userTokens;
    private DbSet<User> _users;
    private DbSet<RefreshToken> _refreshTokens;

    public ForgotPasswordRepository(ClinicContext context)
    {
        _context = context;
        _userTokens = _context.Set<UserToken>();
        _users = _context.Set<User>();
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public async Task<bool> AddResetPasswordTokenCommandAsync(
        UserToken newResetPasswordToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _userTokens.AddAsync(
                entity: newResetPasswordToken,
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

    public Task<bool> IsUserTokenExpiratedByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userTokens.AnyAsync(
            predicate: userToken =>
                userToken.UserId == userId && userToken.ExpiredAt > DateTime.UtcNow,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users.AnyAsync(
            predicate: userDetail =>
                userDetail.Id == userId
                && userDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
