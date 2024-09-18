using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.ChangingPassword;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.ChangingPassword;

/// <summary>
///     Implement for ChangingPassword Repository
/// </summary>
public class ChangingPasswordRepository : IChangingPasswordRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<UserToken> _userTokens;

    public ChangingPasswordRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _userTokens = _context.Set<UserToken>();
    }

    public Task<UserToken> FindUserTokenByResetPasswordTokenQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    )
    {
        return _userTokens
            .AsNoTracking()
            .Where(predicate: userToken => userToken.Value == passwordResetToken)
            .Select(selector: userToken => new UserToken
            {
                UserId = userToken.UserId,
                ExpiredAt = userToken.ExpiredAt,
                Value = userToken.Value,
                User = new() { NormalizedEmail = userToken.User.NormalizedEmail }
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsResetPasswordTokenFoundByItsValueQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    )
    {
        return _userTokens.AnyAsync(
            predicate: userToken => userToken.Value.Equals(passwordResetToken),
            cancellationToken: cancellationToken
        );
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

    public async Task<bool> RemoveUserResetPasswordTokenCommandAsync(
        string resetPasswordToken,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _userTokens
                        .Where(predicate: userToken => userToken.Value.Equals(resetPasswordToken))
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _userTokens
                        .Where(predicate: userToken => userToken.ExpiredAt < DateTime.Now)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
