using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Commons.Others;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.Authorization;

/// <summary>
///     Implement for Authorization Repository
/// </summary>
public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly DbSet<Patient> _patients;

    public AuthorizationRepository(ClinicContext clinicContext)
    {
        _context = clinicContext;
        _refreshTokens = _context.Set<RefreshToken>();
        _patients = _context.Set<Patient>();
    }

    public Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens.AnyAsync(
            predicate: refreshToken => refreshToken.AccessTokenId == accessTokenId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _patients.AnyAsync(
            predicate: patient => patient.UserId == userId,
            //&& patient.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            //&& patient.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
