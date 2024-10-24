using Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;
using Clinic.Application.Features.Appointments.UpdateAppointmentStatus;
using Clinic.Domain.Commons.Entities;
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
using Clinic.Domain.Features.Repositories.Admin.RemovedDoctorTemporarily;
using Clinic.Domain.Features.Repositories.Admin.RemoveMedicineTemporarily;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicine;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicineGroupById;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicineTypeById;
using Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAbsentAppointment;
using Clinic.Domain.Features.Repositories.Appointments.GetAppointmentUpcoming;
using Clinic.Domain.Features.Repositories.Appointments.GetRecentAbsent;
using Clinic.Domain.Features.Repositories.Appointments.GetRecentPending;
using Clinic.Domain.Features.Repositories.Appointments.GetUserBookedAppointment;
using Clinic.Domain.Features.Repositories.Appointments.SwitchToCancelAppointment;
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
using Clinic.Domain.Features.Repositories.ChatRooms.SwitchToEndChatRoom;
using Clinic.Domain.Features.Repositories.Doctors.AddDoctor;
using Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForBooking;
using Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForStaff;
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
using Clinic.Domain.Features.Repositories.Feedbacks.SendFeedBack;
using Clinic.Domain.Features.Repositories.Feedbacks.ViewFeedback;
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
using Clinic.Domain.Features.Repositories.ServiceOrders.UpdateStatusItem;
using Clinic.Domain.Features.Repositories.Users.GetAllMedicalReports;
using Clinic.Domain.Features.Repositories.Users.GetConsultationOverview;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.Domain.Features.Repositories.Users.GetRecentMedicalReport;
using Clinic.Domain.Features.Repositories.Users.GetUserMedicalReport;
using Clinic.Domain.Features.Repositories.Users.UpdateUserAvatar;
using Clinic.Domain.Features.Repositories.Users.UpdateUserDescription;
using Clinic.Domain.Features.Repositories.Users.UpdateUserPrivateInfo;
using Clinic.Domain.Features.UnitOfWorks;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Repositories.Admin.CreateMedicine;
using Clinic.MySQL.Repositories.Admin.CreateNewMedicineGroup;
using Clinic.MySQL.Repositories.Admin.CreateNewMedicineType;
using Clinic.MySQL.Repositories.Admin.DeleteMedicineById;
using Clinic.MySQL.Repositories.Admin.DeleteMedicineGroupById;
using Clinic.MySQL.Repositories.Admin.DeleteMedicineTypeById;
using Clinic.MySQL.Repositories.Admin.GetAllDoctor;
using Clinic.MySQL.Repositories.Admin.GetAllMedicine;
using Clinic.MySQL.Repositories.Admin.GetAllMedicineGroup;
using Clinic.MySQL.Repositories.Admin.GetAllMedicineType;
using Clinic.MySQL.Repositories.Admin.GetAllUser;
using Clinic.MySQL.Repositories.Admin.GetAvailableMedicines;
using Clinic.MySQL.Repositories.Admin.GetMedicineById;
using Clinic.MySQL.Repositories.Admin.GetMedicineGroupById;
using Clinic.MySQL.Repositories.Admin.GetMedicineTypeById;
using Clinic.MySQL.Repositories.Admin.RemovedDoctorTemporarily;
using Clinic.MySQL.Repositories.Admin.RemoveMedicineTemporarily;
using Clinic.MySQL.Repositories.Admin.UpdateMedicine;
using Clinic.MySQL.Repositories.Admin.UpdateMedicineGroupById;
using Clinic.MySQL.Repositories.Admin.UpdateMedicineTypeById;
using Clinic.MySQL.Repositories.Appointments.CreateNewAppointment;
using Clinic.MySQL.Repositories.Appointments.GetAbsentAppointment;
using Clinic.MySQL.Repositories.Appointments.GetAppointmentUpcoming;
using Clinic.MySQL.Repositories.Appointments.GetRecentAbsent;
using Clinic.MySQL.Repositories.Appointments.GetRecentPending;
using Clinic.MySQL.Repositories.Appointments.GetUserBookedAppointment;
using Clinic.MySQL.Repositories.Appointments.UpdateUserBookedAppointment;
using Clinic.MySQL.Repositories.Auths.ChangingPassword;
using Clinic.MySQL.Repositories.Auths.ConfirmUserRegistrationEmail;
using Clinic.MySQL.Repositories.Auths.ForgotPassword;
using Clinic.MySQL.Repositories.Auths.Login;
using Clinic.MySQL.Repositories.Auths.LoginByAdmin;
using Clinic.MySQL.Repositories.Auths.LoginWithGoogle;
using Clinic.MySQL.Repositories.Auths.Logout;
using Clinic.MySQL.Repositories.Auths.RefreshAccessToken;
using Clinic.MySQL.Repositories.Auths.RegisterAsUser;
using Clinic.MySQL.Repositories.Auths.ResendUserRegistrationConfirmedEmail;
using Clinic.MySQL.Repositories.Auths.UpdatePasswordUser;
using Clinic.MySQL.Repositories.ChatContents.CreateChatContent;
using Clinic.MySQL.Repositories.ChatContents.GetChatsByChatRoomId;
using Clinic.MySQL.Repositories.ChatRooms.AssignChatRoom;
using Clinic.MySQL.Repositories.ChatRooms.GetChatRoomsByDoctorId;
using Clinic.MySQL.Repositories.ChatRooms.GetChatRoomsByUserId;
using Clinic.MySQL.Repositories.ChatRooms.RemoveChatContentTemporarily;
using Clinic.MySQL.Repositories.ChatRooms.SwitchToCancelAppointment;
using Clinic.MySQL.Repositories.ChatRooms.SwitchToEndChatRoom;
using Clinic.MySQL.Repositories.Doctor.AddDoctor;
using Clinic.MySQL.Repositories.Doctor.GetAllDoctorForBooking;
using Clinic.MySQL.Repositories.Doctor.GetAllDoctorForStaff;
using Clinic.MySQL.Repositories.Doctor.GetAllMedicalReport;
using Clinic.MySQL.Repositories.Doctor.GetAppointmentsByDate;
using Clinic.MySQL.Repositories.Doctor.GetAvailableDoctor;
using Clinic.MySQL.Repositories.Doctor.GetIdsDoctor;
using Clinic.MySQL.Repositories.Doctor.GetMedicalReportById;
using Clinic.MySQL.Repositories.Doctor.GetProfileDoctor;
using Clinic.MySQL.Repositories.Doctor.GetRecentBookedAppointments;
using Clinic.MySQL.Repositories.Doctor.GetRecentMedicalReportByUserId;
using Clinic.MySQL.Repositories.Doctor.GetUserInforById;
using Clinic.MySQL.Repositories.Doctor.GetUserNotification;
using Clinic.MySQL.Repositories.Doctor.GetUsersHaveMedicalReport;
using Clinic.MySQL.Repositories.Doctor.UpdateDoctorAchievementRepository;
using Clinic.MySQL.Repositories.Doctor.UpdateDoctorDescription;
using Clinic.MySQL.Repositories.Doctor.UpdateDutyStatusRepository;
using Clinic.MySQL.Repositories.Doctor.UpdatePrivateDoctorInfoRepository;
using Clinic.MySQL.Repositories.Enums.GetAllAppointmentStatus;
using Clinic.MySQL.Repositories.Enums.GetAllGender;
using Clinic.MySQL.Repositories.Enums.GetAllPosition;
using Clinic.MySQL.Repositories.Enums.GetAllRetreatmentType;
using Clinic.MySQL.Repositories.Enums.GetAllSpecialty;
using Clinic.MySQL.Repositories.ExaminationServices.CreateService;
using Clinic.MySQL.Repositories.ExaminationServices.GetAllServices;
using Clinic.MySQL.Repositories.ExaminationServices.GetAvailableServices;
using Clinic.MySQL.Repositories.ExaminationServices.GetDetailService;
using Clinic.MySQL.Repositories.ExaminationServices.HiddenService;
using Clinic.MySQL.Repositories.ExaminationServices.RemoveService;
using Clinic.MySQL.Repositories.ExaminationServices.UpdateService;
using Clinic.MySQL.Repositories.Feedbacks.SendFeedBack;
using Clinic.MySQL.Repositories.Feedbacks.ViewFeedback;
using Clinic.MySQL.Repositories.MedicalReports.CreateMedicalReport;
using Clinic.MySQL.Repositories.MedicalReports.UpdateMainInformation;
using Clinic.MySQL.Repositories.MedicalReports.UpdatePatientInformation;
using Clinic.MySQL.Repositories.MedicineOrders.GetMedicineOrderItems;
using Clinic.MySQL.Repositories.MedicineOrders.OrderMedicines;
using Clinic.MySQL.Repositories.MedicineOrders.RemoveOrderItems;
using Clinic.MySQL.Repositories.MedicineOrders.UpdateNoteMedicineOrder;
using Clinic.MySQL.Repositories.MedicineOrders.UpdateOrderItems;
using Clinic.MySQL.Repositories.Notification.CreateRetreatmentNotification;
using Clinic.MySQL.Repositories.OnlinePayments.CreateNewOnlinePayment;
using Clinic.MySQL.Repositories.OnlinePayments.CreateQueueRoom;
using Clinic.MySQL.Repositories.OnlinePayments.GetAllQueueRooms;
using Clinic.MySQL.Repositories.OnlinePayments.GetQueueRoomByUserId;
using Clinic.MySQL.Repositories.OnlinePayments.HandleRedirectURL;
using Clinic.MySQL.Repositories.OnlinePayments.RemoveQueueRoom;
using Clinic.MySQL.Repositories.Schedules.CreateSchedules;
using Clinic.MySQL.Repositories.Schedules.GetDoctorMonthlyDate;
using Clinic.MySQL.Repositories.Schedules.GetDoctorScheduleByDate;
using Clinic.MySQL.Repositories.Schedules.GetSchedulesByDate;
using Clinic.MySQL.Repositories.Schedules.GetSchedulesDateByMonth;
using Clinic.MySQL.Repositories.Schedules.RemoveAllSchedules;
using Clinic.MySQL.Repositories.Schedules.RemoveSchedule;
using Clinic.MySQL.Repositories.Schedules.UpdateSchedule;
using Clinic.MySQL.Repositories.ServiceOrders.AddOrderService;
using Clinic.MySQL.Repositories.ServiceOrders.GetServiceOrderItems;
using Clinic.MySQL.Repositories.Users.GetAllMedicalReports;
using Clinic.MySQL.Repositories.Users.GetConsultationOverview;
using Clinic.MySQL.Repositories.Users.GetProfileUser;
using Clinic.MySQL.Repositories.Users.GetRecentMedicalReport;
using Clinic.MySQL.Repositories.Users.GetUserMedicalReport;
using Clinic.MySQL.Repositories.Users.UpdateUserAvatar;
using Clinic.MySQL.Repositories.Users.UpdateUserDescription;
using Clinic.MySQL.Repositories.Users.UpdateUserPrivateInfo;
using Microsoft.AspNetCore.Identity;

namespace Clinic.MySQL.UnitOfWorks;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ClinicContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    private ILoginRepository _loginRepository;
    private ILogoutRepository _logoutRepository;
    private IForgotPasswordRepository _forgotPasswordRepository;
    private IChangingPasswordRepository _changingPasswordRepository;
    private IRefreshAccessTokenRepository _refreshAccessTokenRepository;
    private IGetProfileUserRepository _getProfileUserRepository;
    private IGetProfileDoctorRepository _getProfileDoctorRepository;
    private IRegisterAsUserRepository _registerAsUserRepository;
    private IConfirmUserRegistrationEmailRepository _confirmUserRegistrationEmailRepository;
    private IUpdatePrivateDoctorInfoRepository _updatePrivateDoctorInfoRepository;
    private IUpdateDoctorDescriptionRepository _updateDoctorDescriptionRepository;
    private IResendUserRegistrationConfirmedEmailRepository _resendUserRegistrationConfirmedEmailRepository;
    private ILoginByAdminRepository _loginByAdminRepository;
    private ILoginWithGoogleRepository _loginWithGoogleRepository;
    private IUpdatePasswordUserRepository _updatePasswordUserRepository;
    private IUpdateDoctorAchievementRepository _updateDoctorAchievementRepository;
    private IUpdateUserAvatarRepository _updateUserAvatarRepository;
    private IUpdateUserPrivateInfoRepository _updateUserPrivateInfoRepository;
    private IGetAllDoctorsRepository _getAllDoctorRepository;
    private IUpdateUserDescriptionRepository _updateUserDescriptionRepository;
    private IAddDoctorRepository _addDoctorRepository;
    private IGetAllUsersRepository _getAllUsersRepository;
    private IGetAllAppointmentStatusRepository _getAllAppointmentStatusRepository;
    private IGetAllGenderRepository _getAllGenderRepository;
    private IGetAllSpecialtyRepository _getAllSpecialtyRepository;
    private IGetAllPositionRepository _getAllPositionRepository;
    private IGetAllRetreatmentTypeRepository _getAllRetreatmentTypeRepository;
    private ICreateSchedulesRepository _createSchedulesRepository;
    private IGetSchedulesByDateRepository _getSchedulesByDateRepository;
    private IGetAllDoctorForBookingRepository _getAllDoctorForBookingRepository;
    private ICreateNewAppointmentRepository _createNewAppointmentRepository;
    private ICreateNewOnlinePaymentRepository _createNewOnlinePaymentRepository;
    private IGetAppointmentsByDateRepository _getAppointmentsByDateRepository;
    private IUpdateDutyStatusRepository _updateDutyStatusRepository;
    private IGetUserBookedAppointmentRepository _getUserBookedAppointmentRepository;
    private IUpdateAppointmentDepositPaymentRepository _updateAppointmentDepositPaymentRepository;
    private IGetAllMedicalReportRepository _getAllMedicalReportRepository;
    private IGetScheduleDatesByMonthRepository _getScheduleDatesByMonthRepository;
    private IGetMedicalReportByIdRepository _getMedicalReportByIdRepository;
    private IGetAppointmentUpcomingRepository _getAppointmentUpcomingRepository;
    private IGetRecentBookedAppointmentsRepository _getRecentBookedAppointmentsRepository;
    private IGetAvailableDoctorRepository _getAvailableDoctorRepository;
    private IUpdateScheduleByIdRepository _updateScheduleByIdRepository;
    private IGetAbsentAppointmentRepository _getAbsentAppointmentRepository;
    private IRemoveScheduleRepository _removeScheduleRepository;
    private IRemoveAllSchedulesRepository _removeAllSchedulesRepository;
    private IGetRecentMedicalReportRepository _getRecentMedicalReportRepository;
    private IGetConsultationOverviewRepository _getConsultationOverviewRepository;
    private ICreateMedicalReportRepository _createMedicalReportRepository;
    private IUpdateUserBookedAppointmentRepository _updateUserBookedAppointmentRepository;
    private ICreateMedicineRepository _createMedicineRepository;
    private IUpdateAppointmentStatusRepository _updateAppointmentStatusRepository;
    private IUpdatePatientInformationRepository _updateMedicalReportPatientInformationRepository;
    private IUpdateMainInformationRepository _updateMainMedicalReportInformationRepository;
    private ICreateServiceRepository _createServiceRepository;
    private IHandleRedirectURLRepository _handleRedirectURLRepository;
    private IGetAllMedicineRepository _getAllMedicineRepository;
    private IGetMedicineByIdRepository _getMedicineByIdRepository;
    private IUpdateMedicineRepository _updateMedicineRepository;
    private IGetAllServicesRepository _getAllServiceRepository;
    private ICreateQueueRoomRepository _createQueueRoomRepository;
    private IDeleteMedicineByIdRepository _deleteMedicineByIdRepository;
    private IUpdateServiceRepository _updateServiceRepository;
    private IGetDetailServiceRepository _getDetailServiceRepository;
    private IRemoveServiceRepository _removeServiceRepository;
    private IAssignChatRoomRepository _assignChatRoomRepository;
    private IHiddenServiceRepository _hiddenServiceRepository;
    private IGetAllMedicineTypeRepository _getAllMedicineTypeRepository;
    private IGetAvailableServicesRepository _getAvailableServicesRepository;
    private IGetAllMedicineGroupRepository _getAllMedicineGroupRepository;
    private ICreateNewMedicineTypeRepository _createNewMedicineTypeRepository;
    private IGetServiceOrderItemsRepository _getServiceOrderItemsRepository;
    private ICreateChatContentRepository _createChatContentRepository;
    private IRemoveChatContentTemporarilyRepository _removeChatContentTemporarilyRepository;
    private ICreateNewMedicineGroupRepository _createNewMedicineGroupRepository;
    private IUpdateMedicineTypeByIdRepository _updateMedicineTypeByIdRepository;
    private IGetAllQueueRoomsRepository _getAllQueueRoomsRepository;
    private IGetChatsByChatRoomIdRepository _getChatsByChatRoomIdRepository;
    private IUpdateMedicineGroupByIdRepository _updateMedicineGroupByIdRepository;
    private IAddOrderServiceRepository _addOrderServiceRepository;
    private IDeleteMedicineTypeByIdRepository _deleteMedicineTypeByIdRepository;
    private IGetQueueRoomByUserIdRepository _getQueueRoomByUserIdRepository;
    private IRemoveQueueRoomRepository _removeQueueRoomRepository;
    private IDeleteMedicineGroupByIdRepository _deleteMedicineGroupByIdRepository;
    private IGetMedicineTypeByIdRepository _getMedicineTypeByIdRepository;
    private IGetMedicineGroupByIdRepository _getMedicineGroupByIdRepository;
    private IRemoveMedicineTemporarilyRepository _removeMedicineTemporarilyRepository;
    private IGetAvailableMedicinesRepository _getAvailableMedicineRepository;
    private IGetChatRoomsByUserIdRepository _getChatRoomsByUserIdRepository;
    private IGetChatRoomsByDoctorIdRepository _getChatRoomsByDoctorIdRepository;
    private ICreateRetreatmentNotificationRepository _createRetreatmentNotificationRepository;
    private IGetMedicineOrderItemsRepostitory _getMedicineOrderItemsRepostitory;
    private IOrderMedicinesRepostitory _orderMedicinesRepostitory;
    private IUpdateMedicineOrderItemRepository _updateMedicineOrderItemRepository;
    private IRemoveMedicineOrderItemRepository _removeMedicineOrderItemRepository;
    private IGetAllUserMedicalReportsRepository _getAllUserMedicalReportsRepository;
    private IUpdateNoteMedicineOrderRepository _updateNoteMedicineOrderRepository;
    private IGetUserNotificationRepository _getUserNotificationRepository;
    private IGetRecentMedicalReportByUserIdRepository _getRecentMedicalReportByUserIdRepository;
    private IGetUserInforByIdRepository _getUserInforByIdRepository;
    private IGetUsersHaveMedicalReportRepository _getUsersHaveMedicalReportRepository;
    private ISwitchToCancelAppointmentRepository _switchToCancelAppointmentRepository;
    private IGetIdsDoctorRepository _getIdsDoctorRepository;
    private ISendFeedBackRepository _sendFeedBackRepository;
    private IGetDoctorMonthlyDateRepository _getDoctorMonthlyDateRepository;
    private IGetDoctorScheduleByDateRepository _getDoctorScheduleByDateRepository;
    private IGetUserMedicalReportRepository _getUserMedicalReportRepository;
    private IGetAllDoctorForStaffRepository _getAllDoctorForStaffRepository;
    private IViewFeedbackRepository _viewFeedbackRepository;
    private IUpdateStatusServiceOrderItemRepository _updateStatusServiceOrderItemRepository;
    private ISwitchToEndChatRoomRepository _switchToEndChatRoomRepository;
    private IRemovedDoctorTemporarilyRepository _removedDoctorTemporarilyRepository;
    private IGetRecentAbsentRepository _getRecentAbsentRepository;
    private IGetRecentPendingRepository _getRecentPendingRepository;

    public UnitOfWork(
        ClinicContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager
    )
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public ILoginRepository LoginRepository
    {
        get { return _loginRepository ??= new LoginRepository(_context); }
    }

    public ILogoutRepository LogoutRepository
    {
        get { return _logoutRepository ??= new LogoutRepository(_context); }
    }

    public IForgotPasswordRepository ForgotPasswordRepository
    {
        get { return _forgotPasswordRepository ??= new ForgotPasswordRepository(_context); }
    }

    public IChangingPasswordRepository ChangingPasswordRepository
    {
        get { return _changingPasswordRepository ??= new ChangingPasswordRepository(_context); }
    }

    public IRefreshAccessTokenRepository RefreshAccessTokenRepository
    {
        get { return _refreshAccessTokenRepository ??= new RefreshAccessTokenRepository(_context); }
    }

    public IGetProfileUserRepository GetProfileUserRepository
    {
        get { return _getProfileUserRepository ??= new GetProfileUserRepository(_context); }
    }

    public IGetAllDoctorsRepository GetAllDoctorRepository
    {
        get { return _getAllDoctorRepository ??= new GetAllDoctorRepository(_context); }
    }

    public IGetProfileDoctorRepository GetProfileDoctorRepository
    {
        get { return _getProfileDoctorRepository ??= new GetProfileDoctorRepository(_context); }
    }

    public IRegisterAsUserRepository RegisterAsUserRepository
    {
        get { return _registerAsUserRepository ??= new RegisterAsUserRepository(_context); }
    }

    public IConfirmUserRegistrationEmailRepository ConfirmUserRegistrationEmailRepository
    {
        get
        {
            return _confirmUserRegistrationEmailRepository ??=
                new ConfirmUserRegistrationEmailRepository(_context);
        }
    }

    public IUpdatePrivateDoctorInfoRepository UpdatePrivateDoctorInfoRepository
    {
        get
        {
            return _updatePrivateDoctorInfoRepository ??= new UpdatePrivateDoctorInfoRepository(
                _context
            );
        }
    }

    public IUpdateDoctorDescriptionRepository UpdateDoctorDescriptionRepository
    {
        get
        {
            return _updateDoctorDescriptionRepository ??= new UpdateDoctorDescriptionRepository(
                _context
            );
        }
    }

    public IResendUserRegistrationConfirmedEmailRepository ResendUserRegistrationConfirmedEmailRepository
    {
        get
        {
            return _resendUserRegistrationConfirmedEmailRepository ??=
                new ResendUserRegistrationConfirmedEmailRepository(_context);
        }
    }

    public ILoginByAdminRepository LoginByAdminRepository
    {
        get { return _loginByAdminRepository ??= new LoginByAdminRepository(_context); }
    }

    public ILoginWithGoogleRepository LoginWithGoogleRepository
    {
        get
        {
            return _loginWithGoogleRepository ??= new LoginWithGoogleRepository(
                _context,
                _userManager
            );
        }
    }

    public IUpdatePasswordUserRepository UpdatePasswordUserRepository
    {
        get
        {
            return _updatePasswordUserRepository ??= new UpdatePasswordUserRepository(
                _context,
                _userManager
            );
        }
    }

    public IUpdateDoctorAchievementRepository UpdateDoctorAchievementRepository
    {
        get
        {
            return _updateDoctorAchievementRepository ??= new UpdateDoctorAchievementRepository(
                _context
            );
        }
    }

    public IUpdateUserAvatarRepository UpdateUserAvatarRepository
    {
        get { return _updateUserAvatarRepository ??= new UpdateUserAvatarRepository(_context); }
    }

    public IUpdateUserPrivateInfoRepository UpdateUserPrivateInfoRepository
    {
        get
        {
            return _updateUserPrivateInfoRepository ??= new UpdateUserPrivateInfoRepository(
                _context
            );
        }
    }

    public IUpdateUserDescriptionRepository UpdateUserDescriptionRepository
    {
        get
        {
            return _updateUserDescriptionRepository ??= new UpdateUserDescriptionRepository(
                _context
            );
        }
    }

    public IAddDoctorRepository AddDoctorRepository
    {
        get { return _addDoctorRepository ??= new AddDoctorRepository(_context, _userManager); }
    }

    public IGetAllUsersRepository GetAllUsersRepository
    {
        get { return _getAllUsersRepository ??= new GetAllUsersRepository(_context); }
    }

    public IGetAllAppointmentStatusRepository GetAllAppointmentStatusRepository
    {
        get
        {
            return _getAllAppointmentStatusRepository ??= new GetAllAppointmentStatusRepository(
                _context
            );
        }
    }

    public IGetAllGenderRepository GetAllGenderRepository
    {
        get { return _getAllGenderRepository ??= new GetAllGenderRepository(_context); }
    }

    public IGetAllSpecialtyRepository GetAllSpecialtyRepository
    {
        get { return _getAllSpecialtyRepository ??= new GetAllSpecialtyRepository(_context); }
    }

    public IGetAllPositionRepository GetAllPositionRepository
    {
        get { return _getAllPositionRepository ??= new GetAllPositionRepository(_context); }
    }

    public IGetAllRetreatmentTypeRepository GetAllRetreatmentTypeRepository
    {
        get
        {
            return _getAllRetreatmentTypeRepository ??= new GetAllRetreatmentTypeRepository(
                _context
            );
        }
    }

    public ICreateSchedulesRepository CreateSchedulesRepository
    {
        get { return _createSchedulesRepository ??= new CreateSchedulesRepository(_context); }
    }

    public IGetSchedulesByDateRepository GetSchedulesByDateRepository
    {
        get { return _getSchedulesByDateRepository ??= new GetSchedulesByDateRepository(_context); }
    }

    public IGetAllDoctorForBookingRepository GetAllDoctorForBookingRepository
    {
        get
        {
            return _getAllDoctorForBookingRepository ??= new GetAllDoctorForBookingRepository(
                _context
            );
        }
    }
    public ICreateNewAppointmentRepository CreateNewAppointmentRepository
    {
        get
        {
            return _createNewAppointmentRepository ??= new CreateNewAppointmentRepository(_context);
        }
    }

    public ICreateNewOnlinePaymentRepository CreateNewOnlinePaymentRepository
    {
        get
        {
            return _createNewOnlinePaymentRepository ??= new CreateNewOnlinePaymentRepository(
                _context
            );
        }
    }
    public IGetAppointmentsByDateRepository GetAppointmentsByDateRepository
    {
        get
        {
            return _getAppointmentsByDateRepository ??= new GetAppointmentsByDateRepository(
                _context
            );
        }
    }

    public IGetUserBookedAppointmentRepository GetUserBookedAppointmentRepository
    {
        get
        {
            return _getUserBookedAppointmentRepository ??= new GetUserBookedAppointmentRepository(
                _context
            );
        }
    }

    public IGetScheduleDatesByMonthRepository GetScheduleDatesByMonthRepository
    {
        get
        {
            return _getScheduleDatesByMonthRepository ??= new GetScheduleDatesByMonthRepository(
                _context
            );
        }
    }

    public IUpdateDutyStatusRepository UpdateDutyStatusRepository
    {
        get { return _updateDutyStatusRepository ??= new UpdateDutyStatusRepository(_context); }
    }

    public IGetRecentBookedAppointmentsRepository GetRecentBookedAppointmentsRepository
    {
        get
        {
            return _getRecentBookedAppointmentsRepository ??=
                new GetRecentBookedAppointmentsRepository(_context);
        }
    }

    public IUpdateAppointmentDepositPaymentRepository UpdateAppointmentDepositPaymentRepository
    {
        get
        {
            return _updateAppointmentDepositPaymentRepository ??=
                new UpdateAppointmentDepositPaymentRepository(_context);
        }
    }
    public IGetAllMedicalReportRepository GetAllMedicalReportRepository
    {
        get
        {
            return _getAllMedicalReportRepository ??= new GetAllMedicalReportRepository(_context);
        }
    }

    public IGetAppointmentUpcomingRepository GetAppointmentUpcomingRepository
    {
        get
        {
            return _getAppointmentUpcomingRepository ??= new GetAppointmentUpcomingRepository(
                _context
            );
        }
    }

    public IGetAvailableDoctorRepository GetAvailableDoctorRepository
    {
        get { return _getAvailableDoctorRepository ??= new GetAvailableDoctorRepository(_context); }
    }

    public IUpdateScheduleByIdRepository UpdateScheduleByIdRepository
    {
        get { return _updateScheduleByIdRepository ??= new UpdateScheduleByIdRepository(_context); }
    }

    public IGetAbsentAppointmentRepository GetAbsentAppointmentRepository
    {
        get
        {
            return _getAbsentAppointmentRepository ??= new GetAbsentAppointmentRepository(_context);
        }
    }

    public IGetMedicalReportByIdRepository GetMedicalReportByIdRepository
    {
        get
        {
            return _getMedicalReportByIdRepository ??= new GetMedicalReportByIdRepository(_context);
        }
    }

    public IRemoveScheduleRepository RemoveScheduleRepository
    {
        get { return _removeScheduleRepository ??= new RemoveScheduleRepository(_context); }
    }

    public IRemoveAllSchedulesRepository RemoveAllSchedulesRepository
    {
        get { return _removeAllSchedulesRepository ??= new RemoveAllSchedulesRepository(_context); }
    }

    public IGetRecentMedicalReportRepository GetRecentMedicalReportRepository
    {
        get
        {
            return _getRecentMedicalReportRepository ??= new GetRecentMedicalReportRepository(
                _context
            );
        }
    }

    public IGetConsultationOverviewRepository GetConsultationOverviewRepository
    {
        get
        {
            return _getConsultationOverviewRepository ??= new GetConsultationOverviewRepository(
                _context
            );
        }
    }

    public ICreateMedicalReportRepository CreateMedicalReportRepository
    {
        get
        {
            return _createMedicalReportRepository ??= new CreateMedicalReportRepository(_context);
        }
    }

    public IUpdateUserBookedAppointmentRepository UpdateUserBookedAppointmentRepository
    {
        get
        {
            return _updateUserBookedAppointmentRepository ??=
                new UpdateUserBookedAppointmentRepository(_context);
        }
    }

    public IUpdateAppointmentStatusRepository UpdateAppointmentStatusRepository
    {
        get
        {
            return _updateAppointmentStatusRepository ??= new UpdateAppointmentStatusRepository(
                _context
            );
        }
    }

    public ICreateMedicineRepository CreateMedicineRepository
    {
        get { return _createMedicineRepository ??= new CreateMedicineRepository(_context); }
    }

    public IHandleRedirectURLRepository HandleRedirectURLRepository
    {
        get { return _handleRedirectURLRepository ??= new HandleRedirectURLRepository(_context); }
    }

    public IGetAllMedicineRepository GetAllMedicineRepository
    {
        get { return _getAllMedicineRepository ??= new GetAllMedicineRepository(_context); }
    }

    public IGetMedicineByIdRepository GetMedicineByIdRepository
    {
        get { return _getMedicineByIdRepository ??= new GetMedicineByIdRepository(_context); }
    }

    public IUpdateMedicineRepository UpdateMedicineRepository
    {
        get { return _updateMedicineRepository ??= new UpdateMedicineRepository(_context); }
    }

    public IUpdatePatientInformationRepository UpdateMedicalReportPatientInformationRepository
    {
        get
        {
            return _updateMedicalReportPatientInformationRepository ??=
                new UpdatePatientInformationRepository(_context);
        }
    }

    public IUpdateMainInformationRepository UpdateMainMedicalReportInformationRepository
    {
        get
        {
            return _updateMainMedicalReportInformationRepository ??=
                new UpdateMainInformationRepository(_context);
        }
    }

    public ICreateServiceRepository CreateServiceRepository
    {
        get { return _createServiceRepository ??= new CreateServiceRepository(_context); }
    }

    public IGetAllServicesRepository GetAllServicesRepository
    {
        get { return _getAllServiceRepository ??= new GetAllServicesRepository(_context); }
    }

    public ICreateQueueRoomRepository CreateQueueRoomRepository
    {
        get { return _createQueueRoomRepository ??= new CreateQueueRoomRepository(_context); }
    }

    public IDeleteMedicineByIdRepository DeleteMedicineByIdRepository
    {
        get { return _deleteMedicineByIdRepository ??= new DeleteMedicineByIdRepository(_context); }
    }

    public IUpdateServiceRepository UpdateServiceRepository
    {
        get { return _updateServiceRepository ??= new UpdateServiceRepository(_context); }
    }

    public IGetDetailServiceRepository GetDetailServiceRepository
    {
        get { return _getDetailServiceRepository ??= new GetDetailServiceRepository(_context); }
    }

    public IRemoveServiceRepository RemoveServiceRepository
    {
        get { return _removeServiceRepository ??= new RemoveServiceRepository(_context); }
    }

    public IAssignChatRoomRepository AssignChatRoomRepository
    {
        get { return _assignChatRoomRepository ??= new AssignChatRoomRepository(_context); }
    }

    public IHiddenServiceRepository HiddenServiceRepository
    {
        get { return _hiddenServiceRepository ??= new HiddenServiceRepository(_context); }
    }

    public IGetAvailableServicesRepository GetAvailableServicesRepository
    {
        get
        {
            return _getAvailableServicesRepository ??= new GetAvailableServicesRepository(_context);
        }
    }

    public IGetAllMedicineTypeRepository GetAllMedicineTypeRepository
    {
        get { return _getAllMedicineTypeRepository ??= new GetAllMedicineTypeRepository(_context); }
    }

    public IRemoveChatContentTemporarilyRepository RemoveChatContentTemporarilyRepository
    {
        get
        {
            return _removeChatContentTemporarilyRepository ??= new RemoveChatTemporarilyRepository(
                _context
            );
        }
    }

    public ICreateChatContentRepository CreateChatContentRepository
    {
        get { return _createChatContentRepository ??= new CreateChatContentRepository(_context); }
    }

    public IGetAllMedicineGroupRepository GetAllMedicineGroupRepository
    {
        get
        {
            return _getAllMedicineGroupRepository ??= new GetAllMedicineGroupRepository(_context);
        }
    }

    public ICreateNewMedicineTypeRepository CreateNewMedicineTypeRepository
    {
        get
        {
            return _createNewMedicineTypeRepository ??= new CreateNewMedicineTypeRepository(
                _context
            );
        }
    }

    public ICreateNewMedicineGroupRepository CreateNewMedicineGroupRepository
    {
        get
        {
            return _createNewMedicineGroupRepository ??= new CreateNewMedicineGroupRepository(
                _context
            );
        }
    }

    public IUpdateMedicineTypeByIdRepository UpdateMedicineTypeByIdRepository
    {
        get
        {
            return _updateMedicineTypeByIdRepository ??= new UpdateMedicineTypeByIdRepository(
                _context
            );
        }
    }
    public IGetAllQueueRoomsRepository GetAllQueueRoomsRepository
    {
        get { return _getAllQueueRoomsRepository ??= new GetAllQueueRoomsRepository(_context); }
    }

    public IGetChatsByChatRoomIdRepository GetChatsByChatRoomIdRepository
    {
        get
        {
            return _getChatsByChatRoomIdRepository ??= new GetChatsByChatRoomIdRepository(_context);
        }
    }

    public IUpdateMedicineGroupByIdRepository UpdateMedicineGroupByIdRepository
    {
        get
        {
            return _updateMedicineGroupByIdRepository ??= new UpdateMedicineGroupByIdRepository(
                _context
            );
        }
    }

    public IDeleteMedicineTypeByIdRepository DeleteMedicineTypeByIdRepository
    {
        get
        {
            return _deleteMedicineTypeByIdRepository ??= new DeleteMedicineTypeByIdRepository(
                _context
            );
        }
    }

    public IGetServiceOrderItemsRepository GetServiceOrderItemsRepository
    {
        get
        {
            return _getServiceOrderItemsRepository ??= new GetServiceOrderItemsRepository(_context);
        }
    }

    public IAddOrderServiceRepository AddOrderServiceRepository
    {
        get { return _addOrderServiceRepository ??= new AddOrderServiceRepository(_context); }
    }

    public IGetQueueRoomByUserIdRepository GetQueueRoomByUserIdRepository
    {
        get
        {
            return _getQueueRoomByUserIdRepository ??= new GetQueueRoomByUserIdRepository(_context);
        }
    }

    public IRemoveQueueRoomRepository RemoveQueueRoomRepository
    {
        get { return _removeQueueRoomRepository ??= new RemoveQueueRoomRepository(_context); }
    }

    public IDeleteMedicineGroupByIdRepository DeleteMedicineGroupByIdRepository
    {
        get
        {
            return _deleteMedicineGroupByIdRepository ??= new DeleteMedicineGroupByIdRepository(
                _context
            );
        }
    }

    public IGetMedicineTypeByIdRepository GetMedicineTypeByIdRepository
    {
        get
        {
            return _getMedicineTypeByIdRepository ??= new GetMedicineTypeByIdRepository(_context);
        }
    }

    public IGetMedicineGroupByIdRepository GetMedicineGroupByIdRepository
    {
        get
        {
            return _getMedicineGroupByIdRepository ??= new GetMedicineGroupByIdRepository(_context);
        }
    }

    public IRemoveMedicineTemporarilyRepository RemoveMedicineTemporarilyRepository
    {
        get
        {
            return _removeMedicineTemporarilyRepository ??= new RemoveMedicineTemporarilyRepository(
                _context
            );
        }
    }

    public IGetAvailableMedicinesRepository GetAvailableMedicinesRepository
    {
        get
        {
            return _getAvailableMedicineRepository ??= new GetAvailableMedicinesRepository(
                _context
            );
        }
    }

    public IGetChatRoomsByUserIdRepository GetChatRoomsByUserIdRepository
    {
        get
        {
            return _getChatRoomsByUserIdRepository ??= new GetChatRoomsByUserIdRepository(_context);
        }
    }

    public IGetChatRoomsByDoctorIdRepository GetChatRoomsByDoctorIdRepository
    {
        get
        {
            return _getChatRoomsByDoctorIdRepository ??= new GetChatRoomsByDoctorIdRepository(
                _context
            );
        }
    }

    public ICreateRetreatmentNotificationRepository CreateRetreatmentNotificationRepository
    {
        get
        {
            return _createRetreatmentNotificationRepository ??=
                new CreateRetreatmentNotificationRepository(_context);
        }
    }

    public IGetMedicineOrderItemsRepostitory GetMedicineOrderItemsRepostitory
    {
        get
        {
            return _getMedicineOrderItemsRepostitory ??= new GetMedicineOrderItemsRepository(
                _context
            );
        }
    }

    public IOrderMedicinesRepostitory OrderMedicinesRepostitory
    {
        get { return _orderMedicinesRepostitory ??= new OrderMedicinesRepository(_context); }
    }

    public IUpdateMedicineOrderItemRepository UpdateMedicineOrderItemRepository
    {
        get
        {
            return _updateMedicineOrderItemRepository ??= new UpdateMedicineOrderItemRepository(
                _context
            );
        }
    }

    public IRemoveMedicineOrderItemRepository RemoveMedicineOrderItemRepository
    {
        get
        {
            return _removeMedicineOrderItemRepository ??= new RemoveMedicineOrderItemRepository(
                _context
            );
        }
    }

    public IGetAllUserMedicalReportsRepository GetAllUserMedicalReportsRepository
    {
        get
        {
            return _getAllUserMedicalReportsRepository ??= new GetAllUserMedicalReportsRepository(
                _context
            );
        }
    }

    public IUpdateNoteMedicineOrderRepository UpdateNoteMedicineOrderRepository
    {
        get
        {
            return _updateNoteMedicineOrderRepository ??= new UpdateNoteMedicineOrderRepository(
                _context
            );
        }
    }
    public IGetUserNotificationRepository GetUserNotificationRepository
    {
        get
        {
            return _getUserNotificationRepository ??= new GetUserNotificationRepository(_context);
        }
    }

    public IGetRecentMedicalReportByUserIdRepository GetRecentMedicalReportByUserIdRepository
    {
        get
        {
            return _getRecentMedicalReportByUserIdRepository ??=
                new GetRecentMedicalReportByUserIdRepository(_context);
        }
    }

    public IGetUserInforByIdRepository GetUserInforByIdRepository
    {
        get { return _getUserInforByIdRepository ??= new GetUserInforByIdRepository(_context); }
    }

    public IGetUsersHaveMedicalReportRepository GetUsersHaveMedicalReportRepository
    {
        get
        {
            return _getUsersHaveMedicalReportRepository ??= new GetUsersHaveMedicalReportRepository(
                _context
            );
        }
    }

    public ISwitchToCancelAppointmentRepository SwitchToCancelAppointmentRepository
    {
        get
        {
            return _switchToCancelAppointmentRepository ??= new SwitchToCancelAppointmentRepository(
                _context
            );
        }
    }

    public IGetIdsDoctorRepository GetIdsDoctorRepository
    {
        get { return _getIdsDoctorRepository ??= new GetIdsDoctorRepository(_context); }
    }

    public ISendFeedBackRepository SendFeedBackRepository
    {
        get { return _sendFeedBackRepository ??= new SendFeedBackRepository(_context); }
    }

    public IGetDoctorMonthlyDateRepository GetDoctorMonthlyDateRepository
    {
        get
        {
            return _getDoctorMonthlyDateRepository ??= new GetDoctorMonthlyDateRepository(_context);
        }
    }

    public IGetDoctorScheduleByDateRepository GetDoctorScheduleByDateRepository
    {
        get
        {
            return _getDoctorScheduleByDateRepository ??= new GetDoctorScheduleByDateRepository(
                _context
            );
        }
    }

    public IGetUserMedicalReportRepository GetUserMedicalReportRepository
    {
        get
        {
            return _getUserMedicalReportRepository ??= new GetUserMedicalReportRepository(_context);
        }
    }

    public IGetAllDoctorForStaffRepository GetAllDoctorForStaffRepository
    {
        get
        {
            return _getAllDoctorForStaffRepository ??= new GetAllDoctorForStaffRepository(_context);
        }
    }

    public IViewFeedbackRepository ViewFeedbackRepository
    {
        get { return _viewFeedbackRepository ??= new ViewFeedbackRepository(_context); }
    }

    public IUpdateStatusServiceOrderItemRepository UpdateStatusServiceOrderItemRepository
    {
        get
        {
            return _updateStatusServiceOrderItemRepository ??=
                new UpdateStatusServiceOrderItemRepository(_context);
        }
    }

    public ISwitchToEndChatRoomRepository SwitchToEndChatRoomRepository
    {
        get
        {
            return _switchToEndChatRoomRepository ??= new SwitchToEndChatRoomRepository(_context);
        }
    }

    public IRemovedDoctorTemporarilyRepository RemovedDoctorTemporarilyRepository
    {
        get
        {
            return _removedDoctorTemporarilyRepository ??= new RemovedDoctorTemporarilyRepository(
                _context
            );
        }
    }
    public IGetRecentAbsentRepository GetRecentAbsentRepository
    {
        get { return _getRecentAbsentRepository ??= new GetRecentAbsentRepository(_context); }
    }

    public IGetRecentPendingRepository GetRecentPendingRepository
    {
        get { return _getRecentPendingRepository ??= new GetRecentPendingRepository(_context); }
    }
}
