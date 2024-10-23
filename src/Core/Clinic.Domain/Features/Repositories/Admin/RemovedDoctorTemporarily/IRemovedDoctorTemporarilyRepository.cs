using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.RemovedDoctorTemporarily;

/// <summary>
///     Interface IRemovedDoctorTemporarilyRepository
/// </summary>
public interface IRemovedDoctorTemporarilyRepository
{
    Task<bool> IsDoctorFoundByIdQueryAsync(Guid doctorId, CancellationToken cancellationToken);

    Task<bool> DeleteDoctorTemporarilyByIdCommandAsync(
        Guid doctorId,
        Guid adminId,
        CancellationToken cancellationToken
    );
}
