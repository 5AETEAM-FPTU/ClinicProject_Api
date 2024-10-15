using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ServiceOrders.GetServiceOrderItems;

public interface IGetServiceOrderItemsRepository
{
    Task<ServiceOrder> GetServiceOrderItemsQueryAsync(Guid serviceOrderId, CancellationToken cancellationToken);
    Task<bool> IsServiceOrderExist(Guid serviceOrderId, CancellationToken CancellationToken);
}
