using Clinic.Domain.Features.Repositories.Auths.ForgotPassword;
using Clinic.Domain.Features.Repositories.Auths.Login;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.Domain.Features.Repositories.Users;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;

namespace Clinic.Domain.Features.UnitOfWorks;

/// <summary>
///     Represent the base unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///    Login repository feature.
    /// </summary>
    public ILoginRepository LoginRepository { get; }

    /// <summary>
    ///    Logout repository feature.
    /// </summary>
    public ILogoutRepository LogoutRepository { get; }

    /// <summary>
    ///    Logout repository feature.
    /// </summary>
    public IForgotPasswordRepository ForgotPasswordRepository { get; }

    /// <summary>
    ///     User repository feature
    /// </summary>
    public IGetProfileUserRepository GetProfileUserRepository { get; }
}
