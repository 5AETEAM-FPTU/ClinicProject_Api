using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Users.GetRecentMedicalReport;

/// <summary>
///     Interface for Query GetRecentMedicalReport Repository
/// </summary>
public interface IGetRecentMedicalReportRepository
{
    Task<IEnumerable<MedicalReport>> FindAllMedicalReportByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
