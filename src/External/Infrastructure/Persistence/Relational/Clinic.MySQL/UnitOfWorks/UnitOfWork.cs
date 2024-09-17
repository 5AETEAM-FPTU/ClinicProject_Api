﻿using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.ChangingPassword;
using Clinic.Domain.Features.Repositories.Auths.ForgotPassword;
using Clinic.Domain.Features.Repositories.Auths.Login;
using Clinic.Domain.Features.Repositories.Auths.Logout;
using Clinic.Domain.Features.Repositories.Auths.RefreshAccessToken;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.Domain.Features.UnitOfWorks;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Repositories.Auths.ChangingPassword;
using Clinic.MySQL.Repositories.Auths.ForgotPassword;
using Clinic.MySQL.Repositories.Auths.Login;
using Clinic.MySQL.Repositories.Auths.Logout;
using Clinic.MySQL.Repositories.Auths.RefreshAccessToken;
using Clinic.MySQL.Repositories.Users.GetProfileUser;
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

<<<<<<< HEAD
    public IGetProfileUserRepository GetProfileUserRepository => throw new System.NotImplementedException();
=======
    public IGetProfileUserRepository GetProfileUserRepository
    {
        get { return _getProfileUserRepository ??= new GetProfileUserRepository(_context); }
    }
>>>>>>> a09b5b6c17c86483569ba8acdfc090b216959f74
}
