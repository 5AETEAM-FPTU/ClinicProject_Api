using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Admin.CreateMedicine;

namespace Clinic.MySQL.Repositories.Admin.CreateMedicine;

internal class CreateMedicineRepository : ICreateMedicineRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Medicine> _medicines;

    public CreateMedicineRepository(ClinicContext context)
    {
        _context = context;
       _medicines = _context.Set<Medicine>();
    }

    public async Task<bool> CreateNewMedicine(Medicine medicine, CancellationToken cancellationToken = default)
    {
        try
        {
            _medicines.Add(medicine);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }
    public async Task<bool> IsExistDrug(string name, CancellationToken cancellationToken = default)
    {
        return await _medicines
                    .AnyAsync(medicine => medicine.Name == name, cancellationToken);
    }
}
