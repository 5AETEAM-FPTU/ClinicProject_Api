using Clinic.Domain.Commons.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Admin.GetMedicineById;


/// <summary>
///     Interface for Query GetMedicineById Repository
/// </summary>
public interface IGetMedicineByIdRepository
{
    Task<Medicine> FindMedicineByIdQueryAsync(
        Guid medicineId,
        CancellationToken cancellationToken);
}
