using Clinic.Domain.Features.Appointments.UpdateAppointmentStatus;
using Clinic.Domain.Features.Repositories.Admin.CreateMedicine;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicine;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineById;
using Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAbsentAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAppointmentUpcoming;
using Clinic.Domain.Features.Repositories.Appointments.GetUserBookedAppointment;
using Clinic.Domain.Features.Repositories.Appointments.UpdateAppointmentDepositPayment;
using Clinic.Domain.Features.Repositories.Appointments.UpdateUserBookedAppointment;
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
using Clinic.Domain.Features.Repositories.Doctors.GetMedicalReportById;
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
using Clinic.Domain.Features.Repositories.ExaminationServices.CreateService;
using Clinic.Domain.Features.Repositories.MedicalReports.CreateMedicalReport;
using Clinic.Domain.Features.Repositories.MedicalReports.UpdateMainInformation;
using Clinic.Domain.Features.Repositories.MedicalReports.UpdatePatientInformation;
using Clinic.Domain.Features.Repositories.OnlinePayments.CreateNewOnlinePayment;
using Clinic.Domain.Features.Repositories.OnlinePayments.HandleRedirectURL;
using Clinic.Domain.Features.Repositories.Schedules.CreateSchedules;
using Clinic.Domain.Features.Repositories.Schedules.GetScheduleDatesByMonth;
using Clinic.Domain.Features.Repositories.Schedules.GetSchedulesByDate;
using Clinic.Domain.Features.Repositories.Schedules.RemoveAllSchedules;
using Clinic.Domain.Features.Repositories.Schedules.RemoveSchedule;
using Clinic.Domain.Features.Repositories.Schedules.UpdateSchedule;
using Clinic.Domain.Features.Repositories.Users.GetAllDoctor;
using Clinic.Domain.Features.Repositories.Users.GetAllUser;
using Clinic.Domain.Features.Repositories.Users.GetConsultationOverview;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.Domain.Features.Repositories.Users.GetRecentMedicalReport;
using Clinic.Domain.Features.Repositories.Users.UpdateUserAvatar;
using Clinic.Domain.Features.Repositories.Users.UpdateUserDescription;
using Clinic.Domain.Features.Repositories.Users.UpdateUserPrivateInfo;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicine;
using Clinic.Domain.Features.Repositories.ExaminationServices.GetAllServices;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineById;
using Clinic.Domain.Features.Repositories.ExaminationServices.UpdateService;
using Clinic.Domain.Features.Repositories.ExaminationServices.GetDetailService;
using Clinic.Domain.Features.Repositories.ExaminationServices.RemoveService;

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

    /// <summary>
    ///    UpdateDutyStatusRepository feature.
    /// </summary>
    public IUpdateDutyStatusRepository UpdateDutyStatusRepository { get; }

    /// <summary>
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

    /// <summary>
    ///    GetScheduleDatesByMonthRepository feature.
    /// </summary>
    public IGetScheduleDatesByMonthRepository GetScheduleDatesByMonthRepository { get; }

    /// <sumary>
    ///    GetMedicalReportByIdRepository feature.
    /// </summary>
    public IGetMedicalReportByIdRepository GetMedicalReportByIdRepository { get; }

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
    ///    UpdateScheduleById feature.
    /// </summary>
    public IUpdateScheduleByIdRepository UpdateScheduleByIdRepository { get; }

    /// <sumary>
    ///    GetAbsentAppointmentRepository feature.
    /// </summary>
    public IGetAbsentAppointmentRepository GetAbsentAppointmentRepository { get; }

    /// <summary>
    ///    RemoveSchedule feature.
    /// </summary>
    public IRemoveScheduleRepository RemoveScheduleRepository { get; }

    /// <summary>
    ///    RemoveAllSchedules feature.
    /// </summary>
    public IRemoveAllSchedulesRepository RemoveAllSchedulesRepository { get; }

    /// <sumary>
    ///    GetRecentMedicalReportRepository feature.
    /// </summary>
    public IGetRecentMedicalReportRepository GetRecentMedicalReportRepository { get; }

    /// <sumary>
    ///    GetConsultationOverviewRepository feature.
    /// </summary>
    public IGetConsultationOverviewRepository GetConsultationOverviewRepository { get; }

    /// <sumary>
    ///    CreateMedicalReportRepository feature.
    /// </summary>
    public ICreateMedicalReportRepository CreateMedicalReportRepository { get; }

    /// <sumary>
    ///    UpdateUserBookedAppointmentRepository feature.
    /// </summary>
    public IUpdateUserBookedAppointmentRepository UpdateUserBookedAppointmentRepository { get; }

    /// <sumary>
    ///    CreateMedicineRepository feature.
    /// </summary>
    public ICreateMedicineRepository CreateMedicineRepository { get; }

    /// <summary>
    ///    UpdateAppointmentStatusRepository feature
    /// </summary>
    public IUpdateAppointmentStatusRepository UpdateAppointmentStatusRepository { get; }

    /// <summary>
    /// CreateService feature
    /// </summary>
    public ICreateServiceRepository CreateServiceRepository { get; }

    /// <summary>
    /// UpdateMedicalReportPatientInformationRepository feature
    /// </summary>
    public IUpdatePatientInformationRepository UpdateMedicalReportPatientInformationRepository { get; }

    /// <summary>
    /// UpdateMainMedicalReportInformationRepository feature
    /// </summary>
    public IUpdateMainInformationRepository UpdateMainMedicalReportInformationRepository { get; }

    ///    HandleRedirectURLRepository feature
    /// </summary>
    public IHandleRedirectURLRepository HandleRedirectURLRepository { get; }

    /// <summary>
    ///    GetAllMedicineRepository feature
    /// </summary>
    public IGetAllMedicineRepository GetAllMedicineRepository { get; }

    /// <summary>
    /// GetMedicineByIdRepository feature
    /// </summary>
    public IGetMedicineByIdRepository GetMedicineByIdRepository { get; }
    
    /// <summary>
    /// UpdateMedicineRepository feature
    /// </summary>
    public IUpdateMedicineRepository UpdateMedicineRepository { get; }

    /// <summary>
    /// Get all services feature
    /// </summary>
    public IGetAllServicesRepository GetAllServicesRepository { get; }
    
    /// <summary>
    /// DeleteMedicineByIdRepository feature
    /// </summary>
    public IDeleteMedicineByIdRepository DeleteMedicineByIdRepository { get; }

    /// <summary>
    /// Update Service feature
    /// </summary>
    public IUpdateServiceRepository UpdateServiceRepository { get; }

    /// <summary>
    /// Get Detail Service feature
    /// </summary>
    public IGetDetailServiceRepository GetDetailServiceRepository { get; }

    /// <summary>
    ///     Remove Service feature
    /// </summary>
    public IRemoveServiceRepository RemoveServiceRepository { get; }
}
