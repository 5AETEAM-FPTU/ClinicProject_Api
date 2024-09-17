using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.RefreshAccessToken;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.RefreshAccessToken;

/// <summary>
///     Implement for RefreshAccessToken Repository
/// </summary>
public class RefreshAccessTokenRepository : IRefreshAccessTokenRepository
{
    private readonly ClinicContext _context;
    private DbSet<RefreshToken> _refreshTokens;

    public RefreshAccessTokenRepository(ClinicContext context)
    {
        _context = context;
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public Task<RefreshToken> FindByRefreshTokenValueQueryAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens
            .AsNoTracking()
            .Where(predicate: refreshToken => refreshToken.RefreshTokenValue == refreshTokenValue)
            .Select(selector: refreshToken => new RefreshToken
            {
                ExpiredDate = refreshToken.ExpiredDate,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        ;
    }

    public async Task<bool> UpdateRefreshTokenCommandAsync(
        string oldRefreshTokenValue,
        string newRefreshTokenValue,
        Guid newAccessTokenId,
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
                            refreshToken.RefreshTokenValue == oldRefreshTokenValue
                        )
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    refreshToken => refreshToken.AccessTokenId,
                                    newAccessTokenId
                                )
                                .SetProperty(
                                    refreshToken => refreshToken.RefreshTokenValue,
                                    newRefreshTokenValue
                                )
                        );
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

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
