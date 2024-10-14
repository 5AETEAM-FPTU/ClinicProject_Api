using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineGroupById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetMedicineGroupById;

/// <summary>
///    Implement of IGetMedicineGroupById repository.
/// </summary>
internal class GetMedicineGroupByIdRepository : IGetMedicineGroupByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineGroup> _medicines;

    public GetMedicineGroupByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<MedicineGroup>();
    }

    public async Task<MedicineGroup> FindMedicineGroupByIdQueryAsync(
        Guid medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines
            .Where(medicine => medicine.Id == medicineGroupId)
            .Select(medicine => new MedicineGroup()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Constant = medicine.Constant,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
