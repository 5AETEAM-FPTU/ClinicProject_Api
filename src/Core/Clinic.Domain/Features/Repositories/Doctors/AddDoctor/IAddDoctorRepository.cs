using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.AddDoctor;

public interface IAddDoctorRepository
{
    Task<bool> IsGenderFoundByIdQueryAsync(Guid genderId, CancellationToken cancellationToken);

    Task<bool> IsPositionFoundByIdQueryAsync(Guid positionId, CancellationToken cancellationToken);

    Task<bool> IsSpecialtyFoundByIdQueryAsync(
        IEnumerable<Guid> specialtyIds,
        CancellationToken cancellationToken
    );

    Task<bool> CreateDoctorCommandAsync(
        User doctor,
        string userPassword,
        string roleName,
        CancellationToken cancellationToken
    );
}
