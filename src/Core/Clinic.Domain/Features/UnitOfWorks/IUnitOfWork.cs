using Clinic.Domain.Features.Appointments.UpdateAppointmentStatus;
using Clinic.Domain.Features.Repositories.Admin.CreateMedicine;
using Clinic.Domain.Features.Repositories.Admin.CreateNewMedicineGroup;
using Clinic.Domain.Features.Repositories.Admin.CreateNewMedicineType;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineById;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineGroupById;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineTypeById;
using Clinic.Domain.Features.Repositories.Admin.GetAllDoctor;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicine;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicineGroup;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicineType;
using Clinic.Domain.Features.Repositories.Admin.GetAllUser;
using Clinic.Domain.Features.Repositories.Admin.GetAvailableMedicines;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineById;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineGroupById;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineTypeById;
using Clinic.Domain.Features.Repositories.Admin.RemoveMedicineTemporarily;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicine;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicineGroupById;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicineTypeById;
using Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAbsentAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAppointmentUpcoming;
using Clinic.Domain.Features.Repositories.Appointments.GetUserBookedAppointment;
using Clinic.Domain.Features.Repositories.Appointments.SwitchToCancelChatRoom;
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
using Clinic.Domain.Features.Repositories.ChatContents.CreateChatContent;
using Clinic.Domain.Features.Repositories.ChatContents.GetChatsByChatRoomId;
using Clinic.Domain.Features.Repositories.ChatContents.RemoveChatContentTemporarily;
using Clinic.Domain.Features.Repositories.ChatRooms.AssignChatRoom;
using Clinic.Domain.Features.Repositories.ChatRooms.GetChatRoomsByDoctorId;
using Clinic.Domain.Features.Repositories.ChatRooms.GetChatRoomsByUserId;
using Clinic.Domain.Features.Repositories.Doctors.AddDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForBooking;
using Clinic.Domain.Features.Repositories.Doctors.GetAllMedicalReport;
using Clinic.Domain.Features.Repositories.Doctors.GetAppointmentsByDate;
using Clinic.Domain.Features.Repositories.Doctors.GetAvailableDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetIdsDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetMedicalReportById;
using Clinic.Domain.Features.Repositories.Doctors.GetProfileDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetRecentBookedAppointments;
using Clinic.Domain.Features.Repositories.Doctors.GetRecentMedicalReportByUserId;
using Clinic.Domain.Features.Repositories.Doctors.GetUserInforById;
using Clinic.Domain.Features.Repositories.Doctors.GetUserNotification;
using Clinic.Domain.Features.Repositories.Doctors.GetUsersHaveMedicalReport;
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
using Clinic.Domain.Features.Repositories.ExaminationServices.GetAllServices;
using Clinic.Domain.Features.Repositories.ExaminationServices.GetAvailableServices;
using Clinic.Domain.Features.Repositories.ExaminationServices.GetDetailService;
using Clinic.Domain.Features.Repositories.ExaminationServices.HiddenService;
using Clinic.Domain.Features.Repositories.ExaminationServices.RemoveService;
using Clinic.Domain.Features.Repositories.ExaminationServices.UpdateService;
using Clinic.Domain.Features.Repositories.MedicalReports.CreateMedicalReport;
using Clinic.Domain.Features.Repositories.MedicalReports.UpdateMainInformation;
using Clinic.Domain.Features.Repositories.MedicalReports.UpdatePatientInformation;
using Clinic.Domain.Features.Repositories.MedicineOrders.GetMedicineOrderItems;
using Clinic.Domain.Features.Repositories.MedicineOrders.OrderMedicines;
using Clinic.Domain.Features.Repositories.MedicineOrders.RemoveOrderItems;
using Clinic.Domain.Features.Repositories.MedicineOrders.UpdateNoteMedicineOrder;
using Clinic.Domain.Features.Repositories.MedicineOrders.UpdateOrderItems;
using Clinic.Domain.Features.Repositories.Notification.CreateRetreatmentNotification;
using Clinic.Domain.Features.Repositories.OnlinePayments.CreateNewOnlinePayment;
using Clinic.Domain.Features.Repositories.OnlinePayments.HandleRedirectURL;
using Clinic.Domain.Features.Repositories.QueueRooms.CreateQueueRoom;
using Clinic.Domain.Features.Repositories.QueueRooms.GetAllQueueRooms;
using Clinic.Domain.Features.Repositories.QueueRooms.GetQueueRoomByUserId;
using Clinic.Domain.Features.Repositories.QueueRooms.RemoveQueueRoom;
using Clinic.Domain.Features.Repositories.Schedules.CreateSchedules;
using Clinic.Domain.Features.Repositories.Schedules.GetDoctorMonthlyDate;
using Clinic.Domain.Features.Repositories.Schedules.GetDoctorScheduleByDate;
using Clinic.Domain.Features.Repositories.Schedules.GetScheduleDatesByMonth;
using Clinic.Domain.Features.Repositories.Schedules.GetSchedulesByDate;
using Clinic.Domain.Features.Repositories.Schedules.RemoveAllSchedules;
using Clinic.Domain.Features.Repositories.Schedules.RemoveSchedule;
using Clinic.Domain.Features.Repositories.Schedules.UpdateSchedule;
using Clinic.Domain.Features.Repositories.ServiceOrders.AddOrderService;
using Clinic.Domain.Features.Repositories.ServiceOrders.GetServiceOrderItems;
using Clinic.Domain.Features.Repositories.Users.GetAllMedicalReports;
using Clinic.Domain.Features.Repositories.Users.GetConsultationOverview;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.Domain.Features.Repositories.Users.GetRecentMedicalReport;
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
    ///    CreateService feature
    /// </summary>
    public ICreateServiceRepository CreateServiceRepository { get; }

    /// <summary>
    ///    UpdateMedicalReportPatientInformationRepository feature
    /// </summary>
    public IUpdatePatientInformationRepository UpdateMedicalReportPatientInformationRepository { get; }

    /// <summary>
    ///    UpdateMainMedicalReportInformationRepository feature
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
    ///    GetMedicineByIdRepository feature
    /// </summary>
    public IGetMedicineByIdRepository GetMedicineByIdRepository { get; }

    /// <summary>
    ///    UpdateMedicineRepository feature
    /// </summary>
    public IUpdateMedicineRepository UpdateMedicineRepository { get; }

    /// <summary>
    ///    Get all services feature
    /// </summary>
    public IGetAllServicesRepository GetAllServicesRepository { get; }

    /// <summary>
    ///    DeleteMedicineByIdRepository feature
    /// </summary>
    public IDeleteMedicineByIdRepository DeleteMedicineByIdRepository { get; }

    /// <summary>
    ///    Update Service feature
    /// </summary>
    public IUpdateServiceRepository UpdateServiceRepository { get; }

    /// <summary>
    ///    CreateQueueRoomRepository feature
    /// </summary>
    public ICreateQueueRoomRepository CreateQueueRoomRepository { get; }

    /// <summary>
    ///    Get Detail Service feature
    /// </summary>
    public IGetDetailServiceRepository GetDetailServiceRepository { get; }

    /// <summary>
    ///     Remove Service (Remove permantly) feature
    /// </summary>
    public IRemoveServiceRepository RemoveServiceRepository { get; }

    /// <summary>
    ///     AssignChatRoomRepository feature
    /// </summary>
    public IAssignChatRoomRepository AssignChatRoomRepository { get; }

    /// </summary>
    ///     Hidden Service (Remove temporarity) feature
    /// </summary>
    public IHiddenServiceRepository HiddenServiceRepository { get; }

    /// <summary>
    ///     GetAllMedicineTypeRepository feature
    /// </summary>
    public IGetAllMedicineTypeRepository GetAllMedicineTypeRepository { get; }

    /// <summary>
    ///    Get Available Services feature
    /// </summary>
    public IGetAvailableServicesRepository GetAvailableServicesRepository { get; }

    /// <summary>
    ///    CreateChatContentRepository feature
    /// </summary>
    public ICreateChatContentRepository CreateChatContentRepository { get; }

    /// <summary>
    ///    RemoveChatContentTemporarilyRepository feature
    /// </summary>
    public IRemoveChatContentTemporarilyRepository RemoveChatContentTemporarilyRepository { get; }

    /// <summary>
    ///     GetAllMedicineGroupRepository feature
    /// </summary>
    public IGetAllMedicineGroupRepository GetAllMedicineGroupRepository { get; }

    /// <summary>
    ///     CreateNewMedicineTypeRepository feature
    /// </summary>
    public ICreateNewMedicineTypeRepository CreateNewMedicineTypeRepository { get; }

    /// <summary>
    ///     GetServiceOrderItems feature
    /// </summary>
    public IGetServiceOrderItemsRepository GetServiceOrderItemsRepository { get; }

    /// <summary>
    ///     CreateNewMedicineGroupRepository feature
    /// </summary>
    public ICreateNewMedicineGroupRepository CreateNewMedicineGroupRepository { get; }

    /// <summary>
    ///     UpdateMedicineTypeByIdRepository feature
    /// </summary>
    public IUpdateMedicineTypeByIdRepository UpdateMedicineTypeByIdRepository { get; }

    /// <summary>
    ///     GetAllQueueRoomsRepository feature
    /// </summary>
    public IGetAllQueueRoomsRepository GetAllQueueRoomsRepository { get; }

    /// <summary>
    ///     GetChatsByChatRoomIdRepository feature
    /// </summary>
    public IGetChatsByChatRoomIdRepository GetChatsByChatRoomIdRepository { get; }

    /// <summary>
    ///     UpdateMedicineGroupByIdRepository feature
    /// </summary>
    public IUpdateMedicineGroupByIdRepository UpdateMedicineGroupByIdRepository { get; }

    /// <summary>
    ///     AddOrderService feature
    /// </summary>
    public IAddOrderServiceRepository AddOrderServiceRepository { get; }

    /// <summary>
    ///     DeleteMedicineTypeByIdRepository feature
    /// </summary>
    public IDeleteMedicineTypeByIdRepository DeleteMedicineTypeByIdRepository { get; }

    ///     GetQueueRoomByUserIdRepository feature
    /// </summary>
    public IGetQueueRoomByUserIdRepository GetQueueRoomByUserIdRepository { get; }

    /// <summary>
    ///     GetQueueRoomByUserIdRepository feature
    /// </summary>
    public IRemoveQueueRoomRepository RemoveQueueRoomRepository { get; }

    /// <summary>
    ///     DeleteMedicineGroupByIdRepository feature
    /// </summary>
    public IDeleteMedicineGroupByIdRepository DeleteMedicineGroupByIdRepository { get; }

    /// <summary>
    ///     GetMedicineTypeByIdRepository feature
    /// </summary>
    public IGetMedicineTypeByIdRepository GetMedicineTypeByIdRepository { get; }

    /// <summary>
    ///     GetMedicineGroupByIdRepository feature
    /// </summary>
    public IGetMedicineGroupByIdRepository GetMedicineGroupByIdRepository { get; }

    /// <summary>
    ///     RemoveMedicineTemporarilyRepository feature
    /// </summary>
    public IRemoveMedicineTemporarilyRepository RemoveMedicineTemporarilyRepository { get; }

    /// <summary>
    ///     GetAvailableMedicinesRepository feature
    /// </summary>
    public IGetAvailableMedicinesRepository GetAvailableMedicinesRepository { get; }

    /// <summary>
    ///     GetAvailableMedicinesRepository feature
    /// </summary>
    public IGetChatRoomsByUserIdRepository GetChatRoomsByUserIdRepository { get; }

    /// <summary>
    ///     GetChatRoomsByDoctorIdRepository feature
    /// </summary>
    public IGetChatRoomsByDoctorIdRepository GetChatRoomsByDoctorIdRepository { get; }

    /// <summary>
    ///     CreateRetreatmentNotificationRepository feature
    /// </summary>
    public ICreateRetreatmentNotificationRepository CreateRetreatmentNotificationRepository { get; }

    /// <summary>
    ///     GetMedicineOrderItems feature
    /// </summary>
    public IGetMedicineOrderItemsRepostitory GetMedicineOrderItemsRepostitory { get; }

    /// <summary>
    ///     GetMedicineOrderItems feature
    /// </summary>
    public IOrderMedicinesRepostitory OrderMedicinesRepostitory { get; }

    /// <summary>
    ///     UpdateMedicineOrderItem feature
    /// </summary>
    public IUpdateMedicineOrderItemRepository UpdateMedicineOrderItemRepository { get; }

    /// <summary>
    ///      RemoveMedicineOrderItem feature
    /// </summary>
    public IRemoveMedicineOrderItemRepository RemoveMedicineOrderItemRepository { get; }

    /// <summary>
    ///      GetAllUserMedicalReports feature
    /// </summary>
    public IGetAllUserMedicalReportsRepository GetAllUserMedicalReportsRepository { get; }

    /// <summary>
    ///      UpdateNoteMedicineOrder feature
    /// </summary>
    public IUpdateNoteMedicineOrderRepository UpdateNoteMedicineOrderRepository { get; }

    /// <summary>
    ///      GetUserNotification feature
    /// </summary>
    public IGetUserNotificationRepository GetUserNotificationRepository { get; }

    /// <summary>
    ///      GetRecentMedicalReportByUserId feature
    /// </summary>
    public IGetRecentMedicalReportByUserIdRepository GetRecentMedicalReportByUserIdRepository { get; }

    /// <summary>
    ///      GetUserInforById feature
    /// </summary>
    public IGetUserInforByIdRepository GetUserInforByIdRepository { get; }

    /// <summary>
    ///      GetUsersHaveMedicalReport feature
    /// </summary>
    public IGetUsersHaveMedicalReportRepository GetUsersHaveMedicalReportRepository { get; }
  
    /// <summary>
    ///      SwitchToCancelChatRoom feature
    /// </summary>
    public ISwitchToCancelChatRoomRepository SwitchToCancelChatRoomRepository { get; }

    /// <summary>
    ///      GetIdsDoctor feature
    /// </summary>
    public IGetIdsDoctorRepository GetIdsDoctorRepository { get; }

    /// <summary>
    ///     GetDoctorMonthlyDate feature
    /// </summary>

    public IGetDoctorMonthlyDateRepository GetDoctorMonthlyDateRepository { get; }

    /// <summary>
    ///     GetDoctorScheduleByDate feature
    /// </summary>
    ///
    public IGetDoctorScheduleByDateRepository GetDoctorScheduleByDateRepository { get; }
}
