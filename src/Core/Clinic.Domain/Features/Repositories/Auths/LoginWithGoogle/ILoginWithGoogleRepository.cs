using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.LoginWithGoogle;

/// <summary>
///     Interface for LoginWithGoogle Repository
/// </summary>
public interface ILoginWithGoogleRepository
{
    Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        string gmail,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserFoundByEmailQueryAsync(string gmail, CancellationToken cancellationToken);

    Task<bool> CreateUserCommandAsync(
        User user,
        string defaultPassword,
        CancellationToken cancellationToken
    );

    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );
}
