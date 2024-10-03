using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetAvailableDoctor;

/// <summary>
///     Interface for Query GetAvailableDoctor Repository
/// </summary>
public interface IGetAvailableDoctorRepository
{
    Task<IEnumerable<User>> GetAvailableDoctorQueryAsync(
        CancellationToken cancellationToken = default
    );
}
