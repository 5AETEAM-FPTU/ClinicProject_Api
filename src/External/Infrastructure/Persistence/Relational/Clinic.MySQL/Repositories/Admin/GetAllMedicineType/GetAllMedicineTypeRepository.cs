using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicineType;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetAllMedicineType;

/// <summary>
///    Implement of IGetAllMedicineType repository.
/// </summary>
internal class GetAllMedicineTypeRepository : IGetAllMedicineTypeRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineType> _medicineTypes;

    public GetAllMedicineTypeRepository(ClinicContext context)
    {
        _context = context;
        _medicineTypes = _context.Set<MedicineType>();
    }

    public async Task<IEnumerable<MedicineType>> FindAllMedicineTypeQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _medicineTypes
            .AsNoTracking()
            .Select(type => new MedicineType()
            {
                Id = type.Id,
                Name = type.Name,
                Constant = type.Constant,
            })
            .OrderBy(type => type.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
