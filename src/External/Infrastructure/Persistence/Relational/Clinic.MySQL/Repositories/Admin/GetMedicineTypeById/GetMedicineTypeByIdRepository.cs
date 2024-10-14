using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineTypeById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetMedicineTypeById;

/// <summary>
///    Implement of IGetMedicineTypeById repository.
/// </summary>
internal class GetMedicineTypeByIdRepository : IGetMedicineTypeByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineType> _medicines;

    public GetMedicineTypeByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<MedicineType>();
    }

    public async Task<MedicineType> FindMedicineTypeByIdQueryAsync(
        Guid medicineTypeId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines
            .Where(medicine => medicine.Id == medicineTypeId)
            .Select(medicine => new MedicineType()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Constant = medicine.Constant,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
