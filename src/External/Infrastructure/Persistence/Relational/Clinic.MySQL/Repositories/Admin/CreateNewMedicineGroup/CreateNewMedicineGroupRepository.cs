using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.CreateNewMedicineGroup;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Admin.CreateNewMedicineGroup;

internal class CreateNewMedicineGroupRepository : ICreateNewMedicineGroupRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicineGroup> _medicines;

    public CreateNewMedicineGroupRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<MedicineGroup>();
    }

    public async Task<bool> CreateNewMedicineGroup(MedicineGroup medicineGroup, CancellationToken cancellationToken = default)
    {
        try
        {
            _medicines.Add(medicineGroup);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }
    public async Task<bool> IsExistMedicineGroup(string constant, CancellationToken cancellationToken = default)
    {
        return await _medicines
                    .AnyAsync(medicine => medicine.Constant == constant, cancellationToken);
    }
}

