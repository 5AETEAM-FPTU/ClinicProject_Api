using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.ConfirmUserRegistrationEmail;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     Implement for ConfirmUserRegistrationEmail Repository
/// </summary>
public class ConfirmUserRegistrationEmailRepository : IConfirmUserRegistrationEmailRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public ConfirmUserRegistrationEmailRepository(ClinicContext context)
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
            predicate: userDetail =>
                userDetail.Id == userId
                && userDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
