using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Admin.GetAllMedicine;

/// <summary>
///     Interface for Query GetAllMedicine Repository
/// </summary>
public interface IGetAllMedicineRepository
{
    Task<IEnumerable<Medicine>> FindAllMedicinesQueryAsync(
        int pageIndex,
        int pageSize,
        string medicineName,
        Guid? medicineTypeId,
        Guid? medicineGroupId,
        CancellationToken cancellationToken);
    Task<int> CountAllMedicinesQueryAsync(
        string medicineName,
        Guid? medicineTypeId,
        Guid? medicineGroupId,
        CancellationToken cancellationToken);
}