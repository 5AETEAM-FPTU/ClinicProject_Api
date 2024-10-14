using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.GetAvailableServices;

public interface IGetAvailableServicesRepository
{
    Task<IEnumerable<Service>> GetAvailableServicesQueryAsync(string key, CancellationToken cancellationToken);
}
