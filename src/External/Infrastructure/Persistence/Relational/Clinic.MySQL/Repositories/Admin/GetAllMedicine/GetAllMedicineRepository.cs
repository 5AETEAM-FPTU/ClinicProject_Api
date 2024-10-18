using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAllMedicine;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetAllMedicine;

/// <summary>
///    Implement of IGetAllMedicine repository.
/// </summary>
internal class GetAllMedicineRepository : IGetAllMedicineRepository
{
    private readonly ClinicContext _context;
    private DbSet<Medicine> _medicines;

    public GetAllMedicineRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<Medicine>();
    }

    public async Task<int> CountAllMedicinesQueryAsync(
        string medicineName,
        Guid? medicineTypeId,
        Guid? medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        var results = _medicines.AsNoTracking().AsQueryable();
        if (medicineName != default)
        {
            results = results.Where(medicine => medicine.Name.Contains(medicineName));
        }

        if (medicineTypeId != default)
        {
            results = results.Where(medicine => medicine.MedicineTypeId == medicineTypeId);
        }

        if (medicineGroupId != default)
        {
            results = results.Where(medicine => medicine.MedicineGroupId == medicineGroupId);
        }
        return await results.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Medicine>> FindAllMedicinesQueryAsync(
        int pageIndex,
        int pageSize,
        string medicineName,
        Guid? medicineTypeId,
        Guid? medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        var results = _medicines.AsNoTracking().AsQueryable();
        if (medicineName != default)
        {
            results = results.Where(medicine => medicine.Name.Contains(medicineName));
        }

        if (medicineTypeId != default)
        {
            results = results.Where(medicine => medicine.MedicineTypeId == medicineTypeId);
        }

        if (medicineGroupId != default)
        {
            results = results.Where(medicine => medicine.MedicineGroupId == medicineGroupId);
        }
        results = results.OrderBy(medicine => medicine.Name);
        return await results
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
                },
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
