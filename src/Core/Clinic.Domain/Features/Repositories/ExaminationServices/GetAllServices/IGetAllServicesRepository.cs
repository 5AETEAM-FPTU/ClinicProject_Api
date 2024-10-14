using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.GetAllServices;

public interface IGetAllServicesRepository
{
    Task<IEnumerable<Service>> FindAllServicesQueryAsync(
        int pageIndex,
        int pageSize,
        string key,
        CancellationToken cancellationToken);
    Task<int> CountAllServicesQueryAsync(
        string key,
        CancellationToken cancellationToken);
}
