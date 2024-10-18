using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.RemoveService;

public interface IRemoveServiceRepository
{
    Task<bool> IsServiceExisted(Guid serviceId, CancellationToken cancellationToken);
    Task<bool> RemoveServiceByIdCommandAsync(Guid Id, CancellationToken cancellationToken);
}
