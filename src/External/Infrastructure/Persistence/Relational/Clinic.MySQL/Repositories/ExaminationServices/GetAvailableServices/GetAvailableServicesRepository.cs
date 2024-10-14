using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ExaminationServices.GetAvailableServices;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.ExaminationServices.GetAvailableServices;

public class GetAvailableServicesRepository : IGetAvailableServicesRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Service> _services;

    public GetAvailableServicesRepository(ClinicContext context)
    {
        _context = context;
        _services = _context.Set<Service>();
    }

    public async Task<IEnumerable<Service>> GetAvailableServicesQueryAsync(string key, CancellationToken cancellationToken)
    {
        var result = _services
            .AsNoTracking()
            .Where(service => 
                service.RemovedAt == CommonConstant.MIN_DATE_TIME
                && service.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID)
            .AsQueryable();

        if(key != default)
        {
            result = result.Where(service => 
                service.Code.Contains(key) 
                || service.Name.Contains(key)
            );
        }

        return await result
            .Select(service => new Service()
            {
                Id = service.Id,
                Name = service.Name,
                Code = service.Code,
                Descripiton = service.Descripiton,
                Price = service.Price,
                Group = service.Group
            })
            .ToListAsync();

    }
}
