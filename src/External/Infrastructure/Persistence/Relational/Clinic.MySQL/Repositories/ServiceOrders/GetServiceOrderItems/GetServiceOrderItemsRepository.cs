using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ServiceOrders.GetServiceOrderItems;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.ServiceOrders.GetServiceOrderItems;

public class GetServiceOrderItemsRepository : IGetServiceOrderItemsRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<ServiceOrder> _serviceOrders;

    public GetServiceOrderItemsRepository(ClinicContext context)
    {
        _context = context;
        _serviceOrders = _context.Set<ServiceOrder>();
    }
    public async Task<ServiceOrder> GetServiceOrderItemsQueryAsync(Guid serviceOrderId, CancellationToken cancellationToken)
    {
        var serviceOrder = await _serviceOrders
            .AsNoTracking()
            .Where(entity => entity.Id == serviceOrderId)
            .Select(entity =>
                new ServiceOrder
                {
                    Id = entity.Id,
                    IsAllUpdated = entity.IsAllUpdated,
                    ServiceOrderItems = entity.ServiceOrderItems.Select(item => new ServiceOrderItem
                    {
                        ServiceId = item.ServiceId,
                        PriceAtOrder = item.PriceAtOrder,
                        IsUpdated = item.IsUpdated,
                        Service = new Service()
                        {
                            Id = item.Service.Id,
                            Code = item.Service.Code,
                            Name = item.Service.Name,
                            Group = item.Service.Group,
                            Descripiton = item.Service.Descripiton
                        }
                    })
                    .ToList()
                }
            )
            .FirstOrDefaultAsync(cancellationToken);

        return serviceOrder;
    }

    public async Task<bool> IsServiceOrderExist(Guid serviceOrderId, CancellationToken CancellationToken)
    {
        return await _serviceOrders.AnyAsync(entity => entity.Id == serviceOrderId);
    }
}
