using Clinic.Domain.Commons.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.CreateNewMedicineType;

public interface ICreateNewMedicineTypeRepository
{

    Task<bool> IsExistMedicineType(string constant, CancellationToken cancellationToken = default);

    Task<bool> CreateNewMedicineType(MedicineType medicineType, CancellationToken cancellationToken = default);
}
