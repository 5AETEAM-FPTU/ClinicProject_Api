using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.Logout;

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

    public async Task<bool> RemoveRefreshTokenCommandAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _refreshTokens
                        .Where(predicate: refreshToken =>
                            refreshToken.AccessTokenId == accessTokenId
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                    await dbTransaction.CommitAsync(cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
