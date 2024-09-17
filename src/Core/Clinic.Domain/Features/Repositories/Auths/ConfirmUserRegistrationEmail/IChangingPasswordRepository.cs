using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     Interface for ConfirmUserRegistrationEmail Repository
/// </summary>
public interface IConfirmUserRegistrationEmailRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
