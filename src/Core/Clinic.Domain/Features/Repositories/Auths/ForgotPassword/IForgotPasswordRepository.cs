using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Auths.ForgotPassword;

/// <summary>
///     Interface for ForgotPassword Repository
/// </summary>
public interface IForgotPasswordRepository
{
    Task<bool> AddResetPasswordTokenCommandAsync(
        UserToken newResetPasswordToken,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
