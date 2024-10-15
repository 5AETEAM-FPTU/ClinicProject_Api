using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.RemoveMedicineTemporarily;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.RemoveMedicineTemporarily;

internal class RemoveMedicineTemporarilyRepository : IRemoveMedicineTemporarilyRepository
{
    private readonly ClinicContext _context;
    private DbSet<Medicine> _medicines;

    public RemoveMedicineTemporarilyRepository(ClinicContext context)
    {
        _context = context;
        _medicines = context.Set<Medicine>();
    }

    public async Task<bool> RemoveMedicineTemporarilyByIdCommandAsync(
        Medicine medicine,
        CancellationToken cancellationToken
    )
    {
        _medicines.Update(medicine);
        return await _context.SaveChangesAsync() > 0;
    }
}
