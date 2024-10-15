using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ServiceOrders.AddOrderService;

public interface IAddOrderServiceRepository
{
    Task<bool> AddServiceIntoServiceOrderCommandAsync(Guid serviceOrderId, IEnumerable<Guid> serviceIds, CancellationToken cancellationToken);
    Task<bool> AreServicesAvailable(IEnumerable<Guid> serviceIds, CancellationToken cancellationToken);
    Task<bool> IsServiceOrderExist(Guid serviceOrderId, CancellationToken CancellationToken);
}
