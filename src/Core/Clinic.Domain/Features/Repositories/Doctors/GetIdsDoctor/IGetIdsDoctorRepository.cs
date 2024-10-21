using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetIdsDoctor;

/// <summary>
///     Interface for Query GetIdsDoctor Repository
/// </summary>
public interface IGetIdsDoctorRepository
{
    Task<IEnumerable<Guid>> FindIdsDoctorQueryAsync(CancellationToken cancellationToken = default);
}
