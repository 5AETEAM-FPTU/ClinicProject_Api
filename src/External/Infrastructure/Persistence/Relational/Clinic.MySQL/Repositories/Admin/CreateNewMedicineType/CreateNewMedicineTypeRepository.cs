using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Admin.CreateNewMedicineType;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Repositories.Admin.CreateNewMedicineType;

internal class CreateNewMedicineTypeRepository : ICreateNewMedicineTypeRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicineType> _medicines;

    public CreateNewMedicineTypeRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<MedicineType>();
    }

    public async Task<bool> CreateNewMedicineType(MedicineType medicineType, CancellationToken cancellationToken = default)
    {
        try
        {
            _medicines.Add(medicineType);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }
    public async Task<bool> IsExistMedicineType(string constant, CancellationToken cancellationToken = default)
    {
        return await _medicines
                    .AnyAsync(medicine => medicine.Constant == constant, cancellationToken);
    }
}

