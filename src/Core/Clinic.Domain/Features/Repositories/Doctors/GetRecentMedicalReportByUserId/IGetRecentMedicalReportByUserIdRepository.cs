using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetRecentMedicalReportByUserId;

public interface IGetRecentMedicalReportByUserIdRepository
{
    Task<IEnumerable<MedicalReport>> FindAllMedicalReportByUserIdQueryAsync(
        int pageIndex,
        int pageSize,
        Guid userId,
        CancellationToken cancellationToken
    );
    Task<int> CountAllMedicalReportByUserIdQueryAsync(CancellationToken cancellationToken);
    Task<bool> IsUserFoundQueryAsync(Guid userId, CancellationToken cancellationToken);
}
