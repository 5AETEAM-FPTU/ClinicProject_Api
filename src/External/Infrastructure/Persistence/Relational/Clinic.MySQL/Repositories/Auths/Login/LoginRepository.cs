﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.Login;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.Login;

/// <summary>
///     Implement for Login Repository
/// </summary>
public class LoginRepository : ILoginRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<RefreshToken> _refreshTokens;

    public LoginRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}