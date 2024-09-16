using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Commons.Others;

/// <summary>
///     Interface for VerifyAccessToken Repository
/// </summary>
public interface IAuthorizationRepository
{
    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
