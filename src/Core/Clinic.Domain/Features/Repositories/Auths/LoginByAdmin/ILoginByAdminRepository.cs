using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.LoginByAdmin;

/// <summary>
///     Interface for LoginByAdmin Repository
/// </summary>
public interface ILoginByAdminRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<User> GetUserByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );
}
