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
using Clinic.Domain.Features.Repositories.Auths.UpdatePasswordUser;
using Clinic.Domain.Features.Repositories.Doctors.AddDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetProfileDoctor;
using Clinic.Domain.Features.Repositories.Doctors.UpdateDoctorAchievement;
using Clinic.Domain.Features.Repositories.Doctors.UpdateDoctorDescription;
using Clinic.Domain.Features.Repositories.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Domain.Features.Repositories.Enums.GetAllAppointmentStatus;
using Clinic.Domain.Features.Repositories.Users.GetAllDoctor;
using Clinic.Domain.Features.Repositories.Users.GetAllUser;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.Domain.Features.Repositories.Users.UpdateUserAvatar;
using Clinic.Domain.Features.Repositories.Users.UpdateUserDescription;
using Clinic.Domain.Features.Repositories.Users.UpdateUserPrivateInfo;

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
    ///    ChangingPassword repository feature.
    /// </summary>

    public IGetAllDoctorsRepository GetAllDoctorRepository { get; }

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
    ///    UpdatePrivateDoctorInfoRepository repository feature.
    /// </summary>
    public IUpdatePrivateDoctorInfoRepository UpdatePrivateDoctorInfoRepository { get; }

    /// <summary>
    ///    IUpdateDoctorDescriptionRepository repository feature.
    /// </summary>
    public IUpdateDoctorDescriptionRepository UpdateDoctorDescriptionRepository { get; }

    /// <summary>
    ///    ResendUserRegistrationConfirmedEmail repository feature.
    /// </summary>
    public IResendUserRegistrationConfirmedEmailRepository ResendUserRegistrationConfirmedEmailRepository { get; }

    /// <summary>
    ///    LoginByAdmin repository feature.
    /// </summary>
    public ILoginByAdminRepository LoginByAdminRepository { get; }

    /// <summary>
    ///    LoginWithGoogle repository feature.
    /// </summary>
    public ILoginWithGoogleRepository LoginWithGoogleRepository { get; }

    /// <summary>
    ///    UpdatePasswordUser repository feature.
    /// </summary>
    public IUpdatePasswordUserRepository UpdatePasswordUserRepository { get; }

    /// <sumary>
    ///    UpdateDoctorAchievementRepository repository feature.
    /// </summary>
    public IUpdateDoctorAchievementRepository UpdateDoctorAchievementRepository { get; }

    /// <sumary>
    ///    UpdateUserAvatarRepository repository feature.
    /// </summary>
    public IUpdateUserAvatarRepository UpdateUserAvatarRepository { get; }

    /// <sumary>
    ///    UpdateUserPrivateInfoRepository repository feature.
    /// </summary>
    public IUpdateUserPrivateInfoRepository UpdateUserPrivateInfoRepository { get; }

    /// <sumary>
    ///    UpdateUserDescription repository feature.
    /// </summary>
    public IUpdateUserDescriptionRepository UpdateUserDescriptionRepository { get; }

    /// <sumary>
    ///    AddDoctor repository feature.
    /// </summary>
    public IAddDoctorRepository AddDoctorRepository { get; }

    /// <sumary>
    ///    GetAllDoctors Repository feature.
    /// </summary>
    public IGetAllUsersRepository GetAllUsersRepository { get; }

     /// <sumary>
    ///    GetAllAppointmentStatus Repository feature.
    /// </summary>
    public IGetAllAppointmentStatusRepository GetAllAppointmentStatusRepository { get; }

}
