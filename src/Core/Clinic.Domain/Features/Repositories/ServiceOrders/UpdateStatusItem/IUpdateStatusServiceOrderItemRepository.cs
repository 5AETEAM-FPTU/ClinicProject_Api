using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ServiceOrders.UpdateStatusItem;

public interface IUpdateStatusServiceOrderItemRepository
{
    Task<bool> IsServiceItemExist(Guid serviceOrderId, Guid serviceId);
    Task<bool> UpdateStatusServiceOrderItemCommandAsync(Guid serviceOrderId, Guid serviceId, CancellationToken cancellationToken);
}
