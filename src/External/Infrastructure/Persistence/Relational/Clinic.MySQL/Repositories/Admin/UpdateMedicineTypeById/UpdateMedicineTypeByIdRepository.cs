using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicineTypeById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.UpdateMedicineTypeById;

/// <summary>
///    Implement of IUpdateMedicineTypeById repository.
/// </summary>
internal class UpdateMedicineTypeByIdRepository : IUpdateMedicineTypeByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineType> _medicines;

    public UpdateMedicineTypeByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<MedicineType>();
    }

    public async Task<bool> IsMedicineTypeFound(
        Guid medicineTypeId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(
            medicine => medicine.Id == medicineTypeId,
            cancellationToken
        );
    }

    public async Task<bool> UpdateMedicineTypeByIdQueryAsync(
        Guid medicineTypeId,
        string name,
        string constant,
        CancellationToken cancellationToken
    )
    {
        var isUpdatedSuccessfully = false;
        try
        {
            await _medicines
                .Where(predicate: medicine => medicine.Id == medicineTypeId)
                .ExecuteUpdateAsync(setPropertyCalls: builder =>
                    builder
                        .SetProperty(medicine => medicine.Name, name)
                        .SetProperty(medicine => medicine.Constant, constant)
                );

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            isUpdatedSuccessfully = true;
        }
        catch
        {
            isUpdatedSuccessfully = false;
        }
        return isUpdatedSuccessfully;
    }
}
