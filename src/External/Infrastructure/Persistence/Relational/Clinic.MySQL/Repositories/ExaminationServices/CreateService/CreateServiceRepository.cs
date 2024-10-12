using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ExaminationServices.CreateService;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.ExaminationServices.CreateService;

public class CreateServiceRepository : ICreateServiceRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Service> _services;

    public CreateServiceRepository(ClinicContext context)
    {
        _context = context;
        _services = context.Set<Service>();
    }

    public async Task<bool> CreateNewServiceCommandAsync(Service service, CancellationToken cancellationToken)
    {
        try
        {
            _services.Add(service);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }

    public async Task<bool> IsExistServiceCode(string code, CancellationToken cancellationToken)
    {
        return await _services.AnyAsync(entity => entity.Code.Equals(code), cancellationToken);
    }
}
