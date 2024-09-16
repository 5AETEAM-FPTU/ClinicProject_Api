using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.Authorization;

/// <summary>
///     Implement for Logout Repository
/// </summary>
public class LogoutRepository : ILogoutRepository
{
    private readonly ClinicContext _context;
    private DbSet<RefreshToken> _refreshTokens;

    public LogoutRepository(ClinicContext context)
    {
        _context = context;
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public Task<bool> RemoveRefreshTokenCommandAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
