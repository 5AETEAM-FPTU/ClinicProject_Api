using Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAbsentAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAppointmentUpcoming;
using Clinic.Domain.Features.Repositories.Appointments.GetUserBookedAppointment;
using Clinic.Domain.Features.Repositories.Appointments.UpdateAppointmentDepositPayment;
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
using Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForBooking;
using Clinic.Domain.Features.Repositories.Doctors.GetAllMedicalReport;
using Clinic.Domain.Features.Repositories.Doctors.GetAppointmentsByDate;
using Clinic.Domain.Features.Repositories.Doctors.GetAvailableDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetProfileDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetRecentBookedAppointments;
using Clinic.Domain.Features.Repositories.Doctors.UpdateDoctorAchievement;
using Clinic.Domain.Features.Repositories.Doctors.UpdateDoctorDescription;
using Clinic.Domain.Features.Repositories.Doctors.UpdateDutyStatus;
using Clinic.Domain.Features.Repositories.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Domain.Features.Repositories.Enums.GetAllAppointmentStatus;
using Clinic.Domain.Features.Repositories.Enums.GetAllGender;
using Clinic.Domain.Features.Repositories.Enums.GetAllPosition;
using Clinic.Domain.Features.Repositories.Enums.GetAllRetreatmentType;
using Clinic.Domain.Features.Repositories.Enums.GetAllSpecialty;
using Clinic.Domain.Features.Repositories.OnlinePayments.CreateNewOnlinePayment;
using Clinic.Domain.Features.Repositories.Schedules.CreateSchedules;
using Clinic.Domain.Features.Repositories.Schedules.GetScheduleDatesByMonth;
using Clinic.Domain.Features.Repositories.Schedules.GetSchedulesByDate;
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

    /// <sumary>
    ///    GetAllGender Repository feature.
    /// </summary>
    public IGetAllGenderRepository GetAllGenderRepository { get; }

    /// <sumary>
    ///    GetAllSpecialty Repository feature.
    /// </summary>
    public IGetAllSpecialtyRepository GetAllSpecialtyRepository { get; }

    /// <sumary>
    ///    GetAllPositionRepository feature.
    /// </summary>
    public IGetAllPositionRepository GetAllPositionRepository { get; }

    /// <sumary>
    ///    GetAllRetreatmentTypeRepository feature.
    /// </summary>
    public IGetAllRetreatmentTypeRepository GetAllRetreatmentTypeRepository { get; }

    /// <sumary>
    ///    CreateSchedulesRepository feature.
    /// </summary>
    public ICreateSchedulesRepository CreateSchedulesRepository { get; }

    /// <sumary>
    ///    GetSchedulesByDateRepository feature.
    /// </summary>
    public IGetSchedulesByDateRepository GetSchedulesByDateRepository { get; }

    /// <sumary>
    ///    GetAllDoctorForBookingRepository feature.
    /// </summary>
    public IGetAllDoctorForBookingRepository GetAllDoctorForBookingRepository { get; }

    /// <summary>
    ///    CreateNewAppointmentRepository feature.
    /// </summary>
    public ICreateNewAppointmentRepository CreateNewAppointmentRepository { get; }

    /// <summary>
    ///    CreateNewOnlinePaymentRepository feature.
    /// </summary>
    public ICreateNewOnlinePaymentRepository CreateNewOnlinePaymentRepository { get; }

    /// <sumary>
    ///    GetAppointmentsByDateRepository feature.
    /// </summary>
    public IGetAppointmentsByDateRepository GetAppointmentsByDateRepository { get; }

    ///    UpdateDutyStatusRepository feature.
    /// </summary>
    public IUpdateDutyStatusRepository UpdateDutyStatusRepository { get; }

    ///    GetUserBookedAppointmentRepository feature.
    /// </summary>
    public IGetUserBookedAppointmentRepository GetUserBookedAppointmentRepository { get; }

    /// <summary>
    ///     UpdateAppointmentDepositPaymentRepository feature.
    /// </summary>
    public IUpdateAppointmentDepositPaymentRepository UpdateAppointmentDepositPaymentRepository { get; }

    /// <sumary>
    ///    GetAllMedicalReportRepository feature.
    /// </summary>
    public IGetAllMedicalReportRepository GetAllMedicalReportRepository { get; }

    ///    GetScheduleDatesByMonthRepository feature.
    /// </summary>
    public IGetScheduleDatesByMonthRepository GetScheduleDatesByMonthRepository { get; }

    /// <sumary>
    ///    GetRecentBookedAppointmentsRepository feature.
    /// </summary>
    public IGetRecentBookedAppointmentsRepository GetRecentBookedAppointmentsRepository { get; }

    /// <sumary>
    ///    GetAppointmentUpcoming feature.
    /// </summary>
    public IGetAppointmentUpcomingRepository GetAppointmentUpcomingRepository { get; }

    /// <sumary>
    ///    GetAvailableDoctor feature.
    /// </summary>
    public IGetAvailableDoctorRepository GetAvailableDoctorRepository { get; }

    /// <sumary>
    ///    GetAbsentAppointmentRepository feature.
    /// </summary>
    public IGetAbsentAppointmentRepository GetAbsentAppointmentRepository { get; }
}
