using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ExaminationServices.GetDetailService;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.ExaminationServices.GetDetailService;

public class GetDetailServiceRepository : IGetDetailServiceRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Service> _services;

    public GetDetailServiceRepository(ClinicContext context)
    {
        _context = context;
        _services = _context.Set<Service>();
    }

    public async Task<Service> GetDetailServiceByIdQueryAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _services.FirstOrDefaultAsync(service => service.Id == Id, cancellationToken);    
    }

    public async Task<bool> IsServiceExisted(Guid serviceId, CancellationToken cancellationToken)
    {
        return await _services.AnyAsync(service => service.Id == serviceId, cancellationToken);
    }
}
