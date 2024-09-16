using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.ForgotPassword;
using Clinic.Domain.Features.Repositories.Auths.Login;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.Domain.Features.UnitOfWorks;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Repositories.Auths.ForgotPassword;
using Clinic.MySQL.Repositories.Auths.Login;
using Clinic.MySQL.Repositories.Auths.Logout;
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
}
