using Clinic.Domain.Features.Repositories.Auths.ChangingPassword;
using Clinic.Domain.Features.Repositories.Auths.ConfirmUserRegistrationEmail;
using Clinic.Domain.Features.Repositories.Auths.ForgotPassword;
using Clinic.Domain.Features.Repositories.Auths.Login;
using Clinic.Domain.Features.Repositories.Auths.LoginByAdmin;
using Clinic.Domain.Features.Repositories.Auths.LoginWithGoogle;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.Domain.Features.Repositories.Auths.RefreshAccessToken;
using Clinic.Domain.Features.Repositories.Auths.RegisterAsUser;
using Clinic.Domain.Features.Repositories.Auths.ResendUserRegistrationConfirmedEmail;
using Clinic.Domain.Features.Repositories.Enums.GetAllDoctorStaffType;
using Clinic.Domain.Features.Repositories.Users.GetProfileDoctor;
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
    ///    ForgotPassword repository feature.
    /// </summary>
    public IForgotPasswordRepository ForgotPasswordRepository { get; }

    /// <summary>
    ///     User repository feature
    /// </summary>
    public IGetProfileUserRepository GetProfileUserRepository { get; }

    /// <summary>
    ///     Doctor repository feature
    /// </summary>
    public IGetProfileDoctorRepository GetProfileDoctorRepository { get; }

    /// <summary>
    ///     Enums repository feature
    /// </summary>
    public IGetAllDoctorStaffTypeRepository GetAllDoctorStaffTypeRepository { get; }

    /// <summary>
    ///    ChangingPassword repository feature.
    /// </summary>
    public IChangingPasswordRepository ChangingPasswordRepository { get; }

    /// <summary>
    ///    RefreshAccessToken repository feature.
    /// </summary>
    public IRefreshAccessTokenRepository RefreshAccessTokenRepository { get; }

    /// <summary>
    ///    RegisterAsUser repository feature.
    /// </summary>
    public IRegisterAsUserRepository RegisterAsUserRepository { get; }

    /// <summary>
    ///    ConfirmUserRegistrationEmail repository feature.
    /// </summary>
    public IConfirmUserRegistrationEmailRepository ConfirmUserRegistrationEmailRepository { get; }

    /// <summary>
    ///    ResendUserRegistrationConfirmedEmail repository feature.
    /// </summary>
    public IResendUserRegistrationConfirmedEmailRepository ResendUserRegistrationConfirmedEmailRepository { get; }

    /// <summary>
    ///    LoginByAdminRepository repository feature.
    /// </summary>
    public ILoginByAdminRepository LoginByAdminRepository { get; }

    /// <summary>
    ///    LoginWithGoogleRepository repository feature.
    /// </summary>
    public ILoginWithGoogleRepository LoginWithGoogleRepository { get; }
}
