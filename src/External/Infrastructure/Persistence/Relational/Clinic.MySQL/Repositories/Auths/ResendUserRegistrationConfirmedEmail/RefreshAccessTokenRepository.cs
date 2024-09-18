using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.ResendUserRegistrationConfirmedEmail;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     Implement for ResendUserRegistrationConfirmedEmail Repository
/// </summary>
public class ResendUserRegistrationConfirmedEmailRepository
    : IResendUserRegistrationConfirmedEmailRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public ResendUserRegistrationConfirmedEmailRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users.AnyAsync(
            predicate: user =>
                user.Id == userId
                && user.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && user.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
