using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.GetAllMedicalReports;

public interface IGetAllUserMedicalReportsRepository
{
    Task<int> CountAllServicesQueryAsync(string keywork, Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<MedicalReport>> FindAllMedicalReportsByUserIdQueryAsync(int pageIndex, int pageSize, string keyword, Guid userId, CancellationToken cancellationToken);
}
