using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Domain.Features.Repositories.Auths.RegisterAsUser;

/// <summary>
///     Interface for RegisterAsUser Repository
/// </summary>
public interface IRegisterAsUserRepository
{
    Task<bool> CreateUserAndAddUserRoleCommandAsync(
        User newUser,
        string userPassword,
        UserManager<User> userManager,
        string userRole,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserFoundByNormalizedEmailQueryAsync(string email, CancellationToken cancellation);

    Task<bool> IsUserFoundByNormalizedUsernameQueryAsync(
        string username,
        CancellationToken cancellation
    );
}
