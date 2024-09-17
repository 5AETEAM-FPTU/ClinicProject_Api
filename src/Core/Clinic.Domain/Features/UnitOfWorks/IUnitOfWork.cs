using Clinic.Domain.Features.Repositories.Auths.ChangingPassword;
using Clinic.Domain.Features.Repositories.Auths.ForgotPassword;
using Clinic.Domain.Features.Repositories.Auths.Login;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.Domain.Features.Repositories.Auths.RefreshAccessToken;

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
    ///    ForgotPassword repository feature.
    /// </summary>
    public IForgotPasswordRepository ForgotPasswordRepository { get; }

    /// <summary>
    ///    ChangingPassword repository feature.
    /// </summary>
    public IChangingPasswordRepository ChangingPasswordRepository { get; }

    /// <summary>
    ///    RefreshAccessToken repository feature.
    /// </summary>
    public IRefreshAccessTokenRepository RefreshAccessTokenRepository { get; }
}
