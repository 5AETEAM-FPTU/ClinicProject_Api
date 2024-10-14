using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineGroupById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.DeleteMedicineGroupById;

public class DeleteMedicineGroupByIdRepository : IDeleteMedicineGroupByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineGroup> _medicines;

    public DeleteMedicineGroupByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = context.Set<MedicineGroup>();
    }

    public async Task<bool> DeleteMedicineGroupByIdCommandAsync(
        Guid medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        var isDeletedSuccessfully = false;

        try
        {
            await _medicines.Where(medicine => medicine.Id == medicineGroupId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync(cancellationToken);
            isDeletedSuccessfully = true;
        }
        catch
        {
            isDeletedSuccessfully = false;
        }

        return isDeletedSuccessfully;
    }

    public async Task<bool> IsMedicineGroupExist(
        Guid medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(
            medicine => medicine.Id == medicineGroupId,
            cancellationToken
        );
    }
}
