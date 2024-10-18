using Clinic.Domain.Commons.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.CreateNewMedicineGroup;

public interface ICreateNewMedicineGroupRepository
{

    Task<bool> IsExistMedicineGroup(string constant, CancellationToken cancellationToken = default);

    Task<bool> CreateNewMedicineGroup(MedicineGroup medicineGroup, CancellationToken cancellationToken = default);
}
