using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineTypeById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.DeleteMedicineTypeById;

public class DeleteMedicineTypeByIdRepository : IDeleteMedicineTypeByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<MedicineType> _medicines;

    public DeleteMedicineTypeByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = context.Set<MedicineType>();
    }

    public async Task<bool> DeleteMedicineTypeByIdCommandAsync(
        Guid medicineTypeId,
        CancellationToken cancellationToken
    )
    {
        var isDeletedSuccessfully = false;

        try
        {
            await _medicines.Where(medicine => medicine.Id == medicineTypeId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync(cancellationToken);
            isDeletedSuccessfully = true;
        }
        catch
        {
            isDeletedSuccessfully = false;
        }

        return isDeletedSuccessfully;
    }

    public async Task<bool> IsMedicineTypeExist(
        Guid medicineTypeId,
        CancellationToken cancellationToken
    )
    {
        return await _medicines.AnyAsync(
            medicine => medicine.Id == medicineTypeId,
            cancellationToken
        );
    }
}
