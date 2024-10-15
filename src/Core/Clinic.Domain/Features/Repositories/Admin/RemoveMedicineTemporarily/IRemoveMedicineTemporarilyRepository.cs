using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.RemoveMedicineTemporarily;

public interface IRemoveMedicineTemporarilyRepository
{
    Task<bool> RemoveMedicineTemporarilyByIdCommandAsync(
        Medicine medicine,
        CancellationToken cancellationToken
    );
}
