using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Admin.GetMedicineById;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Admin.GetMedicineById;

/// <summary>
///    Implement of IGetMedicineById repository.
/// </summary>
internal class GetMedicineByIdRepository : IGetMedicineByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<Medicine> _medicines;

    public GetMedicineByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<Medicine>();
    }

    public async Task<Medicine> FindMedicineByIdQueryAsync(     
        Guid medicineId,
        CancellationToken cancellationToken)
    {

        return await _medicines
            .Where(medicine => medicine.Id == medicineId)
            .Select(medicine => new Medicine()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Manufacture = medicine.Manufacture,
                Ingredient = medicine.Ingredient,
                MedicineType = new MedicineType()
                {
                    Id = medicine.MedicineType.Id,
                    Name = medicine.MedicineType.Name,
                    Constant = medicine.MedicineType.Constant,
                },
                MedicineGroup = new MedicineGroup()
                {
                    Id = medicine.MedicineGroup.Id,
                    Constant = medicine.MedicineGroup.Constant,
                    Name = medicine.MedicineGroup.Name,
                }
            })
            .FirstOrDefaultAsync(cancellationToken); 
    }
}