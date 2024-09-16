using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.Login;

/// <summary>
///     Interface for Login Repository
/// </summary>
public partial interface ILoginRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<User> GetUserByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );
}
