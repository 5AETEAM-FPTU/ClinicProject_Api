using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.UpdateService;

public interface IUpdateServiceRepository
{
    Task<Service> GetServiceByIdCommandAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsServiceExist(Guid id, CancellationToken cancellationToken);
    Task<bool> IsExistServiceCode(string code, CancellationToken cancellationToken);

    Task<bool> UpdateServiceCommandAsync(Service Service, CancellationToken cancellationToken);
}
