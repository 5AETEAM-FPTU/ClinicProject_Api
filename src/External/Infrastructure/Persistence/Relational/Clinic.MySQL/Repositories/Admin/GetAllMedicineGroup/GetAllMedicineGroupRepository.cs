using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicineGroup;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicineType;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Admin.GetAllMedicineGroup;

/// <summary>
///    Implement of IGetAllMedicineGroup repository.
/// </summary>
internal class GetAllMedicineGroupRepository : IGetAllMedicineGroupRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineGroup> _medicineGroups;

    public GetAllMedicineGroupRepository(ClinicContext context)
    {
        _context = context;
        _medicineGroups = _context.Set<MedicineGroup>();
    }

    public async Task<IEnumerable<MedicineGroup>> FindAllMedicineGroupQueryAsync(CancellationToken cancellationToken)
    {
        return await _medicineGroups
           .AsNoTracking()
           .Select(group => new MedicineGroup()
           {
               Id = group.Id,
               Name = group.Name,
               Constant = group.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
