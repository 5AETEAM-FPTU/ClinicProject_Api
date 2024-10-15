using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAvailableMedicines;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetAvailableMedicines;

/// <summary>
///    Implement of IGetAvailableMedicines repository.
/// </summary>
internal class GetAvailableMedicinesRepository : IGetAvailableMedicinesRepository
{
    private readonly ClinicContext _context;
    private DbSet<Medicine> _medicines;

    public GetAvailableMedicinesRepository(ClinicContext context)
    {
        _context = context;
        _medicines = _context.Set<Medicine>();
    }

    public async Task<IEnumerable<Medicine>> FindAvailableMedicinesQueryAsync(
        string medicineName,
        Guid? medicineTypeId,
        Guid? medicineGroupId,
        CancellationToken cancellationToken
    )
    {
        var results = _medicines
            .AsNoTracking()
            .Where(medicine =>
                medicine.RemovedAt == CommonConstant.MIN_DATE_TIME
                && medicine.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .AsQueryable();
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
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
