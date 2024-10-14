using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.UpdateMedicineGroupById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.UpdateMedicineGroupById;

/// <summary>
///    Implement of IUpdateMedicineGroupById repository.
/// </summary>
internal class UpdateMedicineGroupByIdRepository : IUpdateMedicineGroupByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineGroup> _medicines;

    public UpdateMedicineGroupByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<MedicineGroup>();
    }

    public async Task<bool> IsMedicineGroupFound(
        Guid medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(
            medicine => medicine.Id == medicineGroupId,
            cancellationToken
        );
    }

    public async Task<bool> UpdateMedicineGroupByIdQueryAsync(
        Guid medicineGroupId,
        string name,
        string constant,
        CancellationToken cancellationToken
    )
    {
        var isUpdatedSuccessfully = false;
        try
        {
            await _medicines
                .Where(predicate: medicine => medicine.Id == medicineGroupId)
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
