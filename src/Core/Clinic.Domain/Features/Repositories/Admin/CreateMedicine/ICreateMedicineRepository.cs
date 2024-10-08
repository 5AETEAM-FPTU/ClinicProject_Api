using Clinic.Domain.Commons.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Admin.CreateMedicine;

public interface ICreateMedicineRepository
{

    Task<bool> IsExistDrug(string name, CancellationToken cancellationToken = default);

    Task<bool> CreateNewMedicine(Medicine medicine, CancellationToken cancellationToken = default);
}

