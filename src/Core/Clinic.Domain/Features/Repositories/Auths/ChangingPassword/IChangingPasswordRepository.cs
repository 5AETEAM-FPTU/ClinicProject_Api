using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.ChangingPassword;

/// <summary>
///     Interface for ChangingPassword Repository
/// </summary>
public interface IChangingPasswordRepository
{
    Task<UserToken> FindUserTokenByResetPasswordTokenQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    );

    Task<bool> IsResetPasswordTokenFoundByItsValueQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> RemoveUserResetPasswordTokenCommandAsync(
        string resetPasswordToken,
        CancellationToken cancellationToken
    );
}
