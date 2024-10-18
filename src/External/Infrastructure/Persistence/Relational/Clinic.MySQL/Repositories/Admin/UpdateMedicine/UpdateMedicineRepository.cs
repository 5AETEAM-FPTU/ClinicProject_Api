using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicine;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.UpdateMedicine;

/// <summary>
///    Implement of IUpdateMedicine repository.
/// </summary>
internal class UpdateMedicineRepository : IUpdateMedicineRepository
{
    private readonly ClinicContext _context;
    private DbSet<Medicine> _medicines;

    public UpdateMedicineRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<Medicine>();
    }

    public async Task<bool> IsMedicineFoundById(
        Guid medicineId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(medicine => medicine.Id == medicineId, cancellationToken);
    }

    public async Task<bool> IsMedicineGroupFound(
        Guid medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(
            medicine => medicine.MedicineGroupId == medicineGroupId,
            cancellationToken
        );
    }

    public async Task<bool> IsMedicineTypeFound(
        Guid medicineTypeId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(
            medicine => medicine.MedicineTypeId == medicineTypeId,
            cancellationToken
        );
    }

    public async Task<bool> UpdateMedicineQueryAsync(
        Guid medicineId,
        string MedicineName,
        string Manufacture,
        Guid MedicineGroupId,
        string Ingredient,
        Guid MedicineTypeId,
        CancellationToken cancellationToken
    )
    {
        var isUpdatedSuccessfully = false;
        try
        {
            await _medicines
                .Where(predicate: medicine => medicine.Id == medicineId)
                .ExecuteUpdateAsync(setPropertyCalls: builder =>
                    builder
                        .SetProperty(medicine => medicine.Name, MedicineName)
                        .SetProperty(medicine => medicine.Manufacture, Manufacture)
                        .SetProperty(medicine => medicine.Ingredient, Ingredient)
                        .SetProperty(medicine => medicine.MedicineGroupId, MedicineGroupId)
                        .SetProperty(medicine => medicine.MedicineTypeId, MedicineTypeId)
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
