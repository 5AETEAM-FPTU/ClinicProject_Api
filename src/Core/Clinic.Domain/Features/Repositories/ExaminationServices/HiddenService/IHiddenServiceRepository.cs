using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.HiddenService;

public interface IHiddenServiceRepository
{
    Task<bool> IsServiceExisted(Guid serviceId, CancellationToken cancellationToken);
    Task<bool> RemoveServiceTemporarityCommandAsync(Service service, CancellationToken cancellationToken);
    Task<Service> GetServiceByIdQueryAsync(Guid Id, CancellationToken cancellationToken);
}
