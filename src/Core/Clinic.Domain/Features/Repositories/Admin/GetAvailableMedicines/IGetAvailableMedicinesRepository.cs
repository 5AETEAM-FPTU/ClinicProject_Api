using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.GetAvailableMedicines;

/// <summary>
///     Interface for Query GetAvailableMedicines Repository
/// </summary>
public interface IGetAvailableMedicinesRepository
{
    Task<IEnumerable<Medicine>> FindAvailableMedicinesQueryAsync(
        string medicineName,
        Guid? medicineTypeId,
        Guid? medicineGroupId,
        CancellationToken cancellationToken
    );
}
