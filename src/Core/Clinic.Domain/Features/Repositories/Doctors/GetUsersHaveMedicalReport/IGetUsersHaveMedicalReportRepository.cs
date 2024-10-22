using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetUsersHaveMedicalReport;

public interface IGetUsersHaveMedicalReportRepository
{
    Task<IEnumerable<Patient>> FindUsersHaveMedicalReportsQueryAsync(
        string keyword,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> CountAllUserQueryAsync(string keyword, CancellationToken cancellationToken);
}
