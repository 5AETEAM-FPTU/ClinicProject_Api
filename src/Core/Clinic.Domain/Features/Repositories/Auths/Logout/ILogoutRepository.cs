using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Auths.Logout;

/// <summary>
///     Interface for Logout Repository
/// </summary>
public partial interface ILogoutRepository
{
    Task<bool> RemoveRefreshTokenCommandAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
}
