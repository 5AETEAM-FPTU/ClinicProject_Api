using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.LoginByAdmin;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.LoginByAdmin;

/// <summary>
///     Implement for LoginByAdmin Repository
/// </summary>
public class LoginByAdminRepository : ILoginByAdminRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<RefreshToken> _refreshTokens;

    public LoginByAdminRepository(ClinicContext context)
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

    public Task<User> GetUserByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _users
            .AsNoTracking()
            .Where(predicate: user => user.Id == userId)
            .Select(selector: user => new User()
            {
                Avatar = user.Avatar,
                FullName = user.FullName,
                Email = user.Email,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users.AnyAsync(
            predicate: user =>
                user.Id == userId
                && user.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && user.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
