using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.UpdatePrivateDoctorInfo;

public interface IUpdatePrivateDoctorInfoRepository
{
    Task<bool> IsGenderFoundByIdQueryAsync(Guid genderId, CancellationToken cancellationToken);

    Task<bool> IsPositionFoundByIdQueryAsync(Guid positionId, CancellationToken cancellationToken);

    Task<bool> IsSpecialtyFoundByIdQueryAsync(
        Guid specialtyId,
        CancellationToken cancellationToken
    );

    Task<bool> UpdatePrivateDoctorInfoByIdCommandAsync(
        User user,
        CancellationToken cancellationToken
    );

    public Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken);
}
