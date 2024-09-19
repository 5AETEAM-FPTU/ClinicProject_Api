using Clinic.Domain.Commons.Entities;
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
using Clinic.Domain.Features.Repositories.Users.GetAllDoctor;
using Clinic.Domain.Features.Repositories.Users.GetProfileDoctor;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.Domain.Features.Repositories.Users.UpdatePasswordUser;
using Clinic.Domain.Features.UnitOfWorks;
using Clinic.MySQL.Data.Context;
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
using Clinic.MySQL.Repositories.Doctors.GetProfileDoctor;
using Clinic.MySQL.Repositories.Enums.GetAllDoctorStaffType;
using Clinic.MySQL.Repositories.Users.GetAllDoctor;
using Clinic.MySQL.Repositories.Users.GetProfileUser;
using Clinic.MySQL.Repositories.Users.UpdatePasswordUser;
using Microsoft.AspNetCore.Identity;
using Clinic.Domain.Features.Repositories.Users.UpdateProfileDoctor;
using Clinic.MySQL.Repositories.Users.UpdateDoctorDescription;
using Clinic.MySQL.Repositories.Users.UpdateDoctorAchievementRepository;

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
    private IGetAllDoctorStaffTypeRepository _getAllDoctorStaffTypeRepository;
    private IGetAllDoctorsRepository _getAllDoctorRepository;
    private IUpdatePasswordUserRepository _updatePasswordUserRepository;
    private IUpdateDoctorAchievementRepository _updateDoctorAchievementRepository;
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

    public IGetAllDoctorStaffTypeRepository GetAllDoctorStaffTypeRepository
    {
        get { return _getAllDoctorStaffTypeRepository ??= new GetAllDoctorStaffTypeRepository(_context); }
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
            return _updatePrivateDoctorInfoRepository ??=
                new UpdatePrivateDoctorInfoRepository(_context);
        }
    }

    public IUpdateDoctorDescriptionRepository UpdateDoctorDescriptionRepository
    {
        get
        {
            return _updateDoctorDescriptionRepository ??=
                new UpdateDoctorDescriptionRepository(_context);
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
            return _updateDoctorAchievementRepository ??= new UpdateDoctorAchievementRepository(_context);
        }
    }
}
