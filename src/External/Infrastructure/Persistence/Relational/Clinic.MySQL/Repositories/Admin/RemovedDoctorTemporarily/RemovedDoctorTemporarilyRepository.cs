using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.RemovedDoctorTemporarily;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.RemovedDoctorTemporarily;

/// <summary>
///     RemovedDoctorTemporarily Repository
/// </summary>
internal class RemovedDoctorTemporarilyRepository : IRemovedDoctorTemporarilyRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<Clinic.Domain.Commons.Entities.Doctor> _doctors;

    public RemovedDoctorTemporarilyRepository(ClinicContext context)
    {
        _context = context;
        _users = context.Set<User>();
        _doctors = context.Set<Clinic.Domain.Commons.Entities.Doctor>();
    }

    public async Task<bool> DeleteDoctorTemporarilyByIdCommandAsync(
        Guid doctorId,
        Guid adminId,
        CancellationToken cancellationToken
    )
    {
        var dbTransaction = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken
                );
                try
                {
                    await _users
                        .Where(user => user.Id == doctorId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(entity => entity.RemovedAt, DateTime.Now)
                                .SetProperty(entity => entity.RemovedBy, adminId)
                        );

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransaction = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransaction;
    }

    public Task<bool> IsDoctorFoundByIdQueryAsync(
        Guid doctorId,
        CancellationToken cancellationToken
    )
    {
        return _doctors.AnyAsync(entity => entity.UserId == doctorId, cancellationToken);
    }
}
