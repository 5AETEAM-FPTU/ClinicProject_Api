using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.RefreshAccessToken;

/// <summary>
///     Interface for RefreshAccessToken Repository
/// </summary>
public interface IRefreshAccessTokenRepository
{
    Task<RefreshToken> FindByRefreshTokenValueQueryAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken
    );

    Task<bool> UpdateRefreshTokenCommandAsync(
        string oldRefreshTokenValue,
        string newRefreshTokenValue,
        Guid newAccessTokenId,
        CancellationToken cancellationToken
    );
}
