using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     Interface for ResendUserRegistrationConfirmedEmail Repository
/// </summary>
public interface IResendUserRegistrationConfirmedEmailRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
